using StaffZone.DTOs.Guest;
using StaffZone.DTOs.Room;

namespace StaffZone.DTOs.Booking;

public class BookingDetailsDto
{
	public int Id { get; set; }
	public DateTime CheckInDate { get; set; }
	public DateTime CheckOutDate { get; set; }
	public decimal TotalPrice { get; set; }
	public int NumberOfNights { get; set; }
	
	public RoomDto? Room { get; set; }
	public GuestDto? Guest { get; set; }
}
