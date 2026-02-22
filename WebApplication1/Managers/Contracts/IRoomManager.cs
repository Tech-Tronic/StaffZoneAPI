using StaffZone.DTOs.Room;
using StaffZone.Entities;
using StaffZone.Enums;

namespace StaffZone.Managers.Contracts;

public interface IRoomManager : IGenericManager<RoomDto, Room>
{
	Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync();
	Task<IEnumerable<RoomDto>> GetRoomsByStateAsync(RoomState state);
	Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto);
	Task<bool> UpdateRoomAsync(int id, UpdateRoomDto updateRoomDto);
	Task<bool> ChangeRoomStateAsync(int roomId, RoomState newState);
}
