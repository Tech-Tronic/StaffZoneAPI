using StaffZone.DTOs.Floor;
using StaffZone.Entities;

namespace StaffZone.Managers.Contracts;

public interface IFloorManager : IGenericManager<FloorDto, Floor>
{
	Task<FloorWithRoomsDto?> GetFloorWithRoomsAsync(int id);
	Task<IEnumerable<FloorWithRoomsDto>> GetAllFloorsWithRoomsAsync();
	Task<FloorDto> CreateFloorAsync(int floorNumber);
	Task<bool> UpdateFloorAsync(int id, int newFloorNumber);
}
