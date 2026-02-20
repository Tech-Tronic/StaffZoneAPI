using Microsoft.EntityFrameworkCore;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;

namespace StaffZone.Repos.Repositories;

public class FloorRepository : GenericRepository<Floor>, IFloorRepository
{
	public FloorRepository(StaffZoneContext context) : base(context) { }

	public Task<Floor?> GetFloorWithRoomsAsync(int floorId)
		=> _dbSet
			.AsNoTracking()
			.Include(f => f.Rooms)
			.FirstOrDefaultAsync(f => f.Id == floorId);

	public async Task<IEnumerable<Floor>> GetAllFloorsWithRoomsAsync()
	{
		return await _dbSet
			.AsNoTracking()
			.Include(f => f.Rooms)
			.ToListAsync();
	}
}
