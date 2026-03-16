using AutoMapper;
using StaffZone.DTOs.Room;
using StaffZone.Entities;
using StaffZone.Enums;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;
using StaffZone.Helpers;

namespace StaffZone.Managers.Implementations;

public class RoomManager : GenericManager<RoomDto, Room>, IRoomManager
{
	private readonly IRoomRepository _roomRepository;
	private readonly IFloorRepository _floorRepository;

	public RoomManager(IRoomRepository roomRepository, IFloorRepository floorRepository, IMapper mapper)
		: base(roomRepository, mapper)
	{
		_roomRepository = roomRepository;
		_floorRepository = floorRepository;
	}

	public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync()
	{
		var rooms = await _roomRepository.GetAvailableRoomsAsync();
		return _mapper.Map<IEnumerable<RoomDto>>(rooms);
	}

	public async Task<IEnumerable<RoomDto>> GetRoomsByStateAsync(RoomState state)
	{
		var rooms = state switch
		{
			RoomState.Available => await _roomRepository.GetAvailableRoomsAsync(),
			RoomState.Reserved => await _roomRepository.GetReservedRoomsAsync(),
			RoomState.Maintenance => await _roomRepository.GetMaintenanceRoomsAsync(),
			_ => await _roomRepository.GetAllAsync()
		};
		return _mapper.Map<IEnumerable<RoomDto>>(rooms);
	}

	public async Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto)
	{
		
		if (!Validator.IsValidId(createRoomDto.FloorId))
			throw new ArgumentException("Invalid floor ID. Floor ID must be a positive number.");

		var floor = await _floorRepository.GetByIdAsync(createRoomDto.FloorId);

		if (floor == null)
			throw new KeyNotFoundException($"Floor with ID {createRoomDto.FloorId} does not exist.");
		if (!Validator.IsValidType((int)createRoomDto.Type))
			throw new ArgumentException("This type isn't available");
		if (!Validator.IsValidSize((int)createRoomDto.Size))
			throw new ArgumentException("This size isn't available");

		int roomCount = floor.RoomsCount;
		int floorNumber = floor.FloorNumber;

		if (!Validator.IsValidRoomCount(roomCount))
			throw new ArgumentException($"Floor {floorNumber} is a Full Floor");

		var room = _mapper.Map<Room>(createRoomDto);

		room.State = RoomState.Available;
		room.RoomNumber = RoomNumberCalculator.CalculateRoomNumber(floorNumber, roomCount);
		floor.RoomsCount++;

		await _roomRepository.AddAsync(room);
		await _floorRepository.UpdateAsync(floor.Id, floor);

		return _mapper.Map<RoomDto>(room);
	}

	public async Task<bool> ChangeRoomStateAsync(int roomId, RoomState newState)
	{
		var room = await _roomRepository.GetByIdAsync(roomId);

		if (room == null)
			throw new ArgumentException($"Room with ID {roomId} isn't found.");
		if (!Validator.IsValidState((int)newState))
			throw new ArgumentException($"Not a Valid State.");

		room.State = newState;
		await _roomRepository.UpdateAsync(roomId, room);

		return true;
	}

	public async Task<bool> ChangeRoomTypeAsync(int roomId, RoomType newType)
	{
		var room = await _roomRepository.GetByIdAsync(roomId);

		if (room == null)
			throw new ArgumentException($"Room with ID {roomId} isn't found.");
		if (!Validator.IsValidType((int)newType))
			throw new ArgumentException($"Not a Valid Type.");

		room.Type = newType;
		await _roomRepository.UpdateAsync(roomId, room);

		return true;
	}

	public async Task<bool> ChangeRoomSizeAsync(int roomId, RoomSize newSize)
	{
		var room = await _roomRepository.GetByIdAsync(roomId);

		if (room == null)
			throw new ArgumentException($"Room with ID {roomId} isn't found.");
		if (!Validator.IsValidSize((int)newSize))
			throw new ArgumentException($"Not a Valid Size.");

		room.Size = newSize;
		await _roomRepository.UpdateAsync(roomId, room);

		return true;
	}

	public override async Task<bool> DeleteAsync(int id)
	{
		if (!Validator.IsValidId(id))
			throw new ArgumentException("Invalid room ID. Room ID must be a positive number.");

		var room = await _roomRepository.GetByIdAsync(id);
		if (room == null)
			return false;

		var floor = await _floorRepository.GetByIdAsync(room.FloorId);
		if (floor != null && floor.RoomsCount > 0)
		{
			floor.RoomsCount--;
			await _floorRepository.UpdateAsync(floor.Id, floor);
		}

		await _roomRepository.DeleteAsync(id);
		return true;
	}
}
