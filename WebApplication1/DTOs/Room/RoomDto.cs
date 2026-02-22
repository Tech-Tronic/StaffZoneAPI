using StaffZone.Enums;

namespace StaffZone.DTOs.Room;

public class RoomDto
{
	public int Id { get; set; }
	public int RoomNumber { get; set; }
	public RoomType Type { get; set; }
	public RoomState State { get; set; }
	public int FloorId { get; set; }
}
