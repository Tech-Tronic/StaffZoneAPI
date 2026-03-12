using Microsoft.AspNetCore.Mvc;
using StaffZone.DTOs.Room;
using StaffZone.Enums;
using StaffZone.Managers.Contracts;

namespace StaffZone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
	private readonly IRoomManager _roomManager;

	public RoomController(IRoomManager roomManager)
		=> _roomManager = roomManager;

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomDto>))]
	public async Task<IActionResult> GetAllRooms()
	{
		var rooms = await _roomManager.GetAllAsync();
		return Ok(rooms);
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	public async Task<IActionResult> GetRoomById(int id)
	{
		var room = await _roomManager.GetByIdAsync(id);

		if (room == null)
			return NotFound(new { message = $"Room with ID {id} not found." });

		return Ok(room);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoomDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto createRoomDto)
	{
		try
		{
			var room = await _roomManager.CreateRoomAsync(createRoomDto);
			return CreatedAtAction(
				nameof(GetRoomById),
				new { id = room.Id },
				room);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id}type")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> ChangeRoomType([FromRoute] int id, [FromBody] RoomType type)
	{
		try
		{
			var result = await _roomManager.ChangeRoomTypeAsync(id, type);

			if (!result)
				return NotFound(new { message = $"Room with ID {id} not found." });

			return NoContent();
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id}size")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> ChangeRoomSize([FromRoute] int id, [FromBody] RoomSize size)
	{
		try
		{
			var result = await _roomManager.ChangeRoomSizeAsync(id, size);

			if (!result)
				return NotFound(new { message = $"Room with ID {id} not found." });

			return NoContent();
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id}state")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> ChangeRoomsState([FromRoute] int id, [FromBody] RoomState state)
	{
		try
		{
			var result = await _roomManager.ChangeRoomStateAsync(id, state);

			if (!result)
				return NotFound(new { message = $"Room with ID {id} not found." });

			return NoContent();
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteRoom(int id)
	{
		var result = await _roomManager.DeleteAsync(id);

		if (!result)
			return NotFound(new { message = $"Room with ID {id} not found." });

		return NoContent();
	}
}
