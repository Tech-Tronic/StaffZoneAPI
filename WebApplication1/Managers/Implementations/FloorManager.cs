using AutoMapper;
using StaffZone.DTOs.Floor;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;

namespace StaffZone.Managers.Implementations;

public class FloorManager : GenericManager<FloorDto, Floor>, IFloorManager
{
	private readonly IFloorRepository _floorRepository;

	public FloorManager(IFloorRepository floorRepository, IMapper mapper)
		: base(floorRepository, mapper)
	{
		_floorRepository = floorRepository;
	}

	public async Task<FloorWithRoomsDto?> GetFloorWithRoomsAsync(int id)
	{
		var floor = await _floorRepository.GetFloorWithRoomsAsync(id);
		return _mapper.Map<FloorWithRoomsDto?>(floor);
	}

	public async Task<IEnumerable<FloorWithRoomsDto>> GetAllFloorsWithRoomsAsync()
	{
		var floors = await _floorRepository.GetAllFloorsWithRoomsAsync();
		return _mapper.Map<IEnumerable<FloorWithRoomsDto>>(floors);
	}

	public async Task<FloorDto> CreateFloorAsync(int floorNumber)
	{
		// Business validation
		if (floorNumber <= 0)
			throw new ArgumentException("Floor number must be positive.");

		var floor = new Floor
		{
			FloorNumber = floorNumber
		};

		await _floorRepository.AddAsync(floor);
		return _mapper.Map<FloorDto>(floor);
	}

	public async Task<bool> UpdateFloorAsync(int id, int newFloorNumber)
	{
		var existingFloor = await _floorRepository.GetByIdAsync(id);
		if (existingFloor == null)
			return false;

		// Business validation
		if (newFloorNumber <= 0)
			throw new ArgumentException("Floor number must be positive.");

		var updatedFloor = new Floor
		{
			Id = id,
			FloorNumber = newFloorNumber
		};

		await _floorRepository.UpdateAsync(id, updatedFloor);
		return true;
	}
}
