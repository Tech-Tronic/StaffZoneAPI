using AutoMapper;
using StaffZone.DTOs.Booking;
using StaffZone.Entities;
using StaffZone.Enums;
using StaffZone.Helpers;
using StaffZone.Managers.Contracts;
using StaffZone.Repos.Contracts;

namespace StaffZone.Managers.Implementations;

public class BookingManager : GenericManager<BookingDto, Booking>, IBookingManager
{
	private readonly IBookingRepository _bookingRepository;
	private readonly IRoomRepository _roomRepository;
	private readonly IGuestRepository _guestRepository;

	public BookingManager(
		IBookingRepository bookingRepository,
		IRoomRepository roomRepository,
		IGuestRepository guestRepository,
		IMapper mapper) 
		: base(bookingRepository, mapper)
	{
		_bookingRepository = bookingRepository;
		_roomRepository = roomRepository;
		_guestRepository = guestRepository;
	}

	public async Task<BookingDetailsDto?> GetBookingDetailsAsync(int bookingId)
	{
		var booking = await _bookingRepository.GetBookingWithDetailsAsync(bookingId);
		return _mapper.Map<BookingDetailsDto?>(booking);
	}

	public async Task<IEnumerable<BookingDto>> GetGuestBookingsAsync(int guestId)
	{
		var bookings = await _bookingRepository.GetBookingsByGuestIdAsync(guestId);
		return _mapper.Map<IEnumerable<BookingDto>>(bookings);
	}

	public async Task<IEnumerable<BookingDto>> GetActiveBookingsAsync()
	{
		var bookings = await _bookingRepository.GetActiveBookingsAsync();
		return _mapper.Map<IEnumerable<BookingDto>>(bookings);
	}

	public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
	{
		bool checkOutAfterCheckOut = createBookingDto.CheckInDate >= createBookingDto.CheckOutDate;
		bool presentCheckIn = createBookingDto.CheckInDate < DateTime.Today;

		if (checkOutAfterCheckOut)
			throw new ArgumentException("Check-out date must be after check-in date.");

		if (presentCheckIn)
			throw new ArgumentException("Check-in date cannot be in the past.");

		var room = await _roomRepository.GetByIdAsync(createBookingDto.RoomId);
		if (room == null)
			throw new ArgumentException($"Room with ID {createBookingDto.RoomId} does not exist.");

		var guest = await _guestRepository.GetByIdAsync(createBookingDto.GuestId);
		if (guest == null)
			throw new ArgumentException($"Guest with ID {createBookingDto.GuestId} does not exist.");

		var isAvailable = await CheckAvailabilityAsync(
			createBookingDto.RoomId,
			createBookingDto.CheckInDate,
			createBookingDto.CheckOutDate);

		if (!isAvailable)
			throw new InvalidOperationException("Room is not available for the selected dates.");

		var booking = _mapper.Map<Booking>(createBookingDto);
		booking.TotalPrice = PriceCalculator.CalcBookingPrice(createBookingDto.CheckOutDate, createBookingDto.CheckInDate, room.Type);

		await _bookingRepository.AddAsync(booking);

		room.State = RoomState.Reserved;
		await _roomRepository.UpdateAsync(room.Id, room);

		guest.VisitCount++;
		await _guestRepository.UpdateAsync(guest.Id, guest);

		return _mapper.Map<BookingDto>(booking);
	}

	public async Task<bool> CheckAvailabilityAsync(int roomId, DateTime checkIn, DateTime checkOut)
	{
		var existingBookings = await _bookingRepository.GetBookingsByRoomIdAsync(roomId);

		var hasConflict = existingBookings.Any(b =>
			(checkIn >= b.CheckInDate && checkIn < b.CheckOutDate) ||
			(checkOut > b.CheckInDate && checkOut <= b.CheckOutDate) ||
			(checkIn <= b.CheckInDate && checkOut >= b.CheckOutDate)
		);

		return !hasConflict;
	}
}
