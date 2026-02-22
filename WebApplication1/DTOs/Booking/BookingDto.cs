namespace StaffZone.DTOs.Booking;

public class BookingDto
{
	public int Id { get; set; }
	public DateTime CheckInDate { get; set; }
	public DateTime CheckOutDate { get; set; }
	public decimal TotalPrice { get; set; }
	public int RoomId { get; set; }
	public int GuestId { get; set; }
}
