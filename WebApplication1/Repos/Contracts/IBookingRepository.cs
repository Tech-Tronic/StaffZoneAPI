using StaffZone.Entities;

namespace StaffZone.Repos.Contracts;

public interface IBookingRepository : IGenericRepository<Booking>
{
	Task<IEnumerable<Booking>> GetBookingsByGuestIdAsync(int guestId);
	Task<IEnumerable<Booking>> GetBookingsByRoomIdAsync(int roomId);
	Task<IEnumerable<Booking>> GetActiveBookingsAsync();
	Task<IEnumerable<Booking>> GetUpcomingBookingsAsync();
	Task<Booking?> GetBookingWithDetailsAsync(int bookingId);
	Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);
	Task<IEnumerable<Booking>> GetBookingsByCheckInDateAsync(DateTime checkInDate);
	Task<IEnumerable<Booking>> GetBookingsByCheckOutDateAsync(DateTime checkOutDate);
}
