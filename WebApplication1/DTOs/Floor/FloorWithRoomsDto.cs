using StaffZone.DTOs.Room;

namespace StaffZone.DTOs.Floor;

public class FloorWithRoomsDto
{
	public int Id { get; set; }
	public int FloorNumber { get; set; }
	public List<RoomDto> Rooms { get; set; } = new();
}
