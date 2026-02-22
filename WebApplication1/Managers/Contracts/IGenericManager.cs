namespace StaffZone.Managers.Contracts;

public interface IGenericManager<TDto, TEntity> where TEntity : class
{
	Task<IEnumerable<TDto>> GetAllAsync();
	Task<TDto?> GetByIdAsync(int id);
	Task<bool> DeleteAsync(int id);
}
