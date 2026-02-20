using Microsoft.EntityFrameworkCore;
using StaffZone.Entities;
using StaffZone.Enums;
using StaffZone.Repos.Contracts;

namespace StaffZone.Repos.Repositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
	public RoomRepository(StaffZoneContext context) : base(context) { }

	public Task<IEnumerable<Room>> GetAvailableRoomsAsync()
		=> GetRoomsByStateAsync(RoomState.Available);

	public Task<IEnumerable<Room>> GetReservedRoomsAsync()
		=> GetRoomsByStateAsync(RoomState.Reserved);

	public Task<IEnumerable<Room>> GetMaintenanceRoomsAsync()
		=> GetRoomsByStateAsync(RoomState.Maintenance);

	private async Task<IEnumerable<Room>> GetRoomsByStateAsync(RoomState state)
	{
		return await _dbSet
			.AsNoTracking()
			.Where(r => r.State == state)
			.ToListAsync();
	}
}
