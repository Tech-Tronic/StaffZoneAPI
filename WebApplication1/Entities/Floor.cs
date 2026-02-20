namespace StaffZone.Entities;

public class Floor
{
	public int Id { get; set; }
	public int FloorNumber { get; set; }
	public ICollection<Room> Rooms { get; set; }
}
