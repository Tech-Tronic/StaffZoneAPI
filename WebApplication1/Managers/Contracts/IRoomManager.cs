using StaffZone.DTOs.Room;
using StaffZone.Entities;
using StaffZone.Enums;

namespace StaffZone.Managers.Contracts;

public interface IRoomManager : IGenericManager<RoomDto, Room>
{
	Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync();
	Task<IEnumerable<RoomDto>> GetRoomsByStateAsync(RoomState state);
	Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto);
	Task<bool> ChangeRoomStateAsync(int roomId, RoomState newState);
	Task<bool> ChangeRoomTypeAsync(int roomId, RoomType newType);
	Task<bool> ChangeRoomSizeAsync(int roomId, RoomSize newSize);
}
