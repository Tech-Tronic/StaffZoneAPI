using AutoMapper;
using StaffZone.DTOs.Room;
using StaffZone.Entities;
using StaffZone.Enums;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;

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
		var floor = await _floorRepository.GetByIdAsync(createRoomDto.FloorId);
		if (floor == null)
			throw new ArgumentException($"Floor with ID {createRoomDto.FloorId} does not exist.");

		var room = _mapper.Map<Room>(createRoomDto);
		room.State = RoomState.Available;

		await _roomRepository.AddAsync(room);
		return _mapper.Map<RoomDto>(room);
	}

	public async Task<bool> UpdateRoomAsync(int id, UpdateRoomDto updateRoomDto)
	{
		var existingRoom = await _roomRepository.GetByIdAsync(id);
		if (existingRoom == null)
			return false;

		var floor = await _floorRepository.GetByIdAsync(updateRoomDto.FloorId);
		if (floor == null)
			throw new ArgumentException($"Floor with ID {updateRoomDto.FloorId} does not exist.");

		var updatedRoom = _mapper.Map<Room>(updateRoomDto);
		updatedRoom.Id = id;

		await _roomRepository.UpdateAsync(id, updatedRoom);
		return true;
	}

	public async Task<bool> ChangeRoomStateAsync(int roomId, RoomState newState)
	{
		var room = await _roomRepository.GetByIdAsync(roomId);
		if (room == null)
			return false;

		room.State = newState;
		await _roomRepository.UpdateAsync(roomId, room);
		return true;
	}
}
