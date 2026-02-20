using Microsoft.EntityFrameworkCore;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;

namespace StaffZone.Repos.Repositories;

public class GuestRepository : GenericRepository<Guest>, IGuestRepository
{
	public GuestRepository(StaffZoneContext context) : base(context) { }

	public Task<Guest?> GetGuestByNameAsync(string firstName)
		=> _dbSet
			.AsNoTracking()
			.FirstOrDefaultAsync(g => g.FirstName == firstName);

	public Task<Guest?> GetGuestByEmailAsync(string email)
		=> _dbSet
			.AsNoTracking()
			.FirstOrDefaultAsync(g => g.Email == email);

	public Task<Guest?> GetGuestByPhoneNumberAsync(string phoneNumber)
		=> _dbSet
			.AsNoTracking()
			.FirstOrDefaultAsync(g => g.PhoneNumber == phoneNumber);

	public async Task<IEnumerable<Guest>> GetFrequentGuestsAsync(int minVisitCount)
	{
		return await _dbSet
			.AsNoTracking()
			.Where(g => g.VisitCount >= minVisitCount)
			.OrderByDescending(g => g.VisitCount)
			.ToListAsync();
	}
}
