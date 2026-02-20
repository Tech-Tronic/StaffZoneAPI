namespace StaffZone.Entities;

public class Booking
{
	public int Id { get; set; }
	public DateTime CheckInDate { get; set; }
	public DateTime CheckOutDate { get; set; }
	public decimal TotalPrice { get; set; }

	public int RoomId { get; set; }
	public Room? Room { get; set; }

	public int GuestId { get; set; }
	public Guest? Guest { get; set; }
}
