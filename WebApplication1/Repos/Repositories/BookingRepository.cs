using Microsoft.EntityFrameworkCore;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;

namespace StaffZone.Repos.Repositories;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
	public BookingRepository(StaffZoneContext context) : base(context) { }

	public async Task<IEnumerable<Booking>> GetBookingsByGuestIdAsync(int guestId)
	{
		return await _dbSet
			.AsNoTracking()
			.Where(b => b.GuestId == guestId)
			.OrderByDescending(b => b.CheckInDate)
			.ToListAsync();
	}

	public async Task<IEnumerable<Booking>> GetBookingsByRoomIdAsync(int roomId)
	{
		return await _dbSet
			.AsNoTracking()
			.Where(b => b.RoomId == roomId)
			.OrderByDescending(b => b.CheckInDate)
			.ToListAsync();
	}

	public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
	{
		var today = DateTime.Today;
		return await _dbSet
			.AsNoTracking()
			.Where(b => b.CheckInDate <= today && b.CheckOutDate >= today)
			.Include(b => b.Room)
			.Include(b => b.Guest)
			.ToListAsync();
	}

	public async Task<IEnumerable<Booking>> GetUpcomingBookingsAsync()
	{
		var today = DateTime.Today;
		return await _dbSet
			.AsNoTracking()
			.Where(b => b.CheckInDate > today)
			.OrderBy(b => b.CheckInDate)
			.Include(b => b.Room)
			.Include(b => b.Guest)
			.ToListAsync();
	}

	public Task<Booking?> GetBookingWithDetailsAsync(int bookingId)
		=> _dbSet
			.AsNoTracking()
			.Include(b => b.Room)
				.ThenInclude(r => r.Floor)
			.Include(b => b.Guest)
			.FirstOrDefaultAsync(b => b.Id == bookingId);

	public async Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
	{
		return await _dbSet
			.AsNoTracking()
			.Where(b => b.CheckInDate <= endDate && b.CheckOutDate >= startDate)
			.OrderBy(b => b.CheckInDate)
			.ToListAsync();
	}

	public async Task<IEnumerable<Booking>> GetBookingsByCheckInDateAsync(DateTime checkInDate)
	{
		return await _dbSet
			.AsNoTracking()
			.Where(b => b.CheckInDate <= checkInDate)
			.OrderBy(b => b.CheckInDate)
			.ToListAsync();
	}

	public async Task<IEnumerable<Booking>> GetBookingsByCheckOutDateAsync(DateTime checkOutDate)
	{
		return await _dbSet
			.AsNoTracking()
			.Where(b => b.CheckOutDate <= checkOutDate)
			.OrderBy(b => b.CheckInDate)
			.ToListAsync();
	}
}
