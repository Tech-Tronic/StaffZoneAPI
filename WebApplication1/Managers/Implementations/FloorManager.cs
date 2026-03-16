using AutoMapper;
using StaffZone.DTOs.Floor;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;
using StaffZone.Helpers;

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
		if (!Validator.IsValidId(id))
			throw new ArgumentException("Invalid floor ID. Floor ID must be a positive number.");

		var existingFloor = await _floorRepository.GetByIdAsync(id);
		if (existingFloor == null)
			return false;

		if (newFloorNumber <= 0)
			throw new ArgumentException("Floor number must be positive.");

		var updatedFloor = new Floor
		{
			Id = id,
			FloorNumber = newFloorNumber,
			RoomsCount = existingFloor.RoomsCount,
			Rooms = existingFloor.Rooms
		};

		await _floorRepository.UpdateAsync(id, updatedFloor);
		return true;
	}
}
