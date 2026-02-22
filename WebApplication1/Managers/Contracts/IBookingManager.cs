using StaffZone.DTOs.Booking;
using StaffZone.Entities;

namespace StaffZone.Managers.Contracts;

public interface IBookingManager : IGenericManager<BookingDto, Booking>
{
	Task<BookingDetailsDto?> GetBookingDetailsAsync(int bookingId);
	Task<IEnumerable<BookingDto>> GetGuestBookingsAsync(int guestId);
	Task<IEnumerable<BookingDto>> GetActiveBookingsAsync();
	Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto);
	// Task<bool> CancelBookingAsync(int bookingId);
	Task<bool> CheckAvailabilityAsync(int roomId, DateTime checkIn, DateTime checkOut);
}
