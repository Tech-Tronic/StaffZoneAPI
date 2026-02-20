using StaffZone.Entities;

namespace StaffZone.Repos.Contracts;

public interface IGuestRepository : IGenericRepository<Guest>
{
	Task<Guest?> GetGuestByNameAsync(string firstName);
	Task<Guest?> GetGuestByEmailAsync(string email);
	Task<Guest?> GetGuestByPhoneNumberAsync(string phoneNumber);
	Task<IEnumerable<Guest>> GetFrequentGuestsAsync(int minVisitCount);
}
