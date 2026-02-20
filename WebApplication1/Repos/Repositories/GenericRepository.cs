using Microsoft.EntityFrameworkCore;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;

namespace StaffZone.Repos.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	protected readonly StaffZoneContext _context;
	protected readonly DbSet<T> _dbSet;

	public GenericRepository(StaffZoneContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _dbSet
			.AsNoTracking()
			.ToListAsync();
	}

	public Task<T?> GetByIdAsync(int id)
		=> _dbSet.FindAsync(id).AsTask();

	public async Task AddAsync(T entity)
	{
		await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var entity = await GetByIdAsync(id);
		if (entity == null)
			return;
		_dbSet.Remove(entity);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(int entityId, T updatedEntity)
	{
		var existingEntity = await GetByIdAsync(entityId);
		if (existingEntity == null)
			return;

		_context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
		await _context.SaveChangesAsync();
	}
}
