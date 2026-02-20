namespace StaffZone.Repos.Contracts;

public interface IGenericRepository<T> 
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> GetByIdAsync(int id);
	Task AddAsync (T entity);
	Task UpdateAsync (int id, T entity);
	Task DeleteAsync (int id);
}
