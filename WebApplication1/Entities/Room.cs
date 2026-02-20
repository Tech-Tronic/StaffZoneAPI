using StaffZone.Enums;

namespace StaffZone.Entities;

public class Room
{
	public int Id { get; set; }
	public int RoomNumber { get; set; }
	public RoomType Type { get; set; }
	public RoomState State { get; set; }

	public int FloorId { get; set; }
	public Floor? Floor { get; set; }
}
