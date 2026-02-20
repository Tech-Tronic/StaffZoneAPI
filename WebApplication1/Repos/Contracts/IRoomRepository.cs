using StaffZone.Entities;

namespace StaffZone.Repos.Contracts;


public interface IRoomRepository : IGenericRepository<Room>
{
	Task<IEnumerable<Room>> GetAvailableRoomsAsync();
	Task<IEnumerable<Room>> GetReservedRoomsAsync();
	Task<IEnumerable<Room>> GetMaintenanceRoomsAsync();
}
