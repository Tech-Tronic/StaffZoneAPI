using StaffZone.Entities;

namespace StaffZone.Repos.Contracts;

public interface IFloorRepository : IGenericRepository<Floor> 
{
	Task<Floor?> GetFloorWithRoomsAsync(int floorId);
	Task<IEnumerable<Floor>> GetAllFloorsWithRoomsAsync();
}
