namespace StaffZone.DTOs.Booking;

public class CreateBookingDto
{
	public DateTime CheckInDate { get; set; }
	public DateTime CheckOutDate { get; set; }
	public int RoomId { get; set; }
	public int GuestId { get; set; }
}
