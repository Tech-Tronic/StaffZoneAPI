using StaffZone.Enums;

namespace StaffZone.DTOs.Room;

public class CreateRoomDto
{
	// RoomNumber removed - will be calculated automatically based on floor number
	// public int RoomNumber { get; set; }
	public RoomType Type { get; set; }
	public RoomSize Size { get; set; }
	public int FloorId { get; set; }
}
