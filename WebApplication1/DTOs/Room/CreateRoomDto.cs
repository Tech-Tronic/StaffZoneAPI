using StaffZone.Enums;

namespace StaffZone.DTOs.Room;

public class CreateRoomDto
{
	public int RoomNumber { get; set; }
	public RoomType Type { get; set; }
	public int FloorId { get; set; }
}
