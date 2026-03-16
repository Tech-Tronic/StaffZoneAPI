using AutoMapper;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;
using StaffZone.Helpers;

namespace StaffZone.Managers.Implementations;

public abstract class GenericManager<TDto, TEntity> : IGenericManager<TDto, TEntity> 
	where TEntity : class
	where TDto : class
{
	protected readonly IGenericRepository<TEntity> _repository;
	protected readonly IMapper _mapper;

	protected GenericManager(IGenericRepository<TEntity> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<TDto>> GetAllAsync()
	{
		var entities = await _repository.GetAllAsync();
		return _mapper.Map<IEnumerable<TDto>>(entities);
	}

	public async Task<TDto?> GetByIdAsync(int id)
	{
		if (!Validator.IsValidId(id))
			throw new ArgumentException("Invalid ID. ID must be a positive number.");

		var entity = await _repository.GetByIdAsync(id);
		return _mapper.Map<TDto?>(entity);
	}

	public virtual async Task<bool> DeleteAsync(int id)
	{
		if (!Validator.IsValidId(id))
			throw new ArgumentException("Invalid ID. ID must be a positive number.");

		var entity = await _repository.GetByIdAsync(id);
		if (entity == null)
			return false;

		await _repository.DeleteAsync(id);
		return true;
	}
}
