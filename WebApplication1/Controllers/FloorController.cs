using Microsoft.AspNetCore.Mvc;
using StaffZone.Managers.Contracts;
using StaffZone.DTOs.Floor;

namespace StaffZone.Controllers;

[Route("api/[controller]")]
[ApiController] // Automatic HTTP 400 responses
public class FloorController : ControllerBase
{
	private readonly IFloorManager _floorManager;

	public FloorController(IFloorManager floorManager)
		=> _floorManager = floorManager;


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FloorDto>))]
	public async Task<IActionResult> GetAllFloors()
	{
		var floors = await _floorManager.GetAllAsync();
		return Ok(floors);
	}

	[HttpGet("with-rooms")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FloorWithRoomsDto>))]
	public async Task<IActionResult> GetAllFloorsWithRooms()
	{
		var floors = await _floorManager.GetAllFloorsWithRoomsAsync();
		return Ok(floors);
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FloorDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetFloorById(int id)
	{
		var floor = await _floorManager.GetByIdAsync(id);

		if (floor == null)
			return NotFound(new { message = $"Floor with ID {id} not found." });

		return Ok(floor);
	}

	[HttpGet("{id}/with-rooms")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FloorWithRoomsDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetFloorWithRooms(int id)
	{
		var floor = await _floorManager.GetFloorWithRoomsAsync(id);

		if (floor == null)
			return NotFound(new { message = $"Floor with ID {id} not found." });

		return Ok(floor);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FloorDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateFloor([FromBody] int floorNumber)
	{
		try
		{
			var floor = await _floorManager.CreateFloorAsync(floorNumber);
			return CreatedAtAction(
				nameof(GetFloorById),
				new { id = floor.Id },
				floor);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> UpdateFloor(int id, [FromBody] int newFloorNumber)
	{
		try
		{
			var result = await _floorManager.UpdateFloorAsync(id, newFloorNumber);

			if (!result)
				return NotFound(new { message = $"Floor with ID {id} not found." });

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
	public async Task<IActionResult> DeleteFloor(int id)
	{
		var result = await _floorManager.DeleteAsync(id);

		if (!result)
			return NotFound(new { message = $"Floor with ID {id} not found." });

		return NoContent();
	}

	[HttpGet("{id}/rooms/count")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetRoomCount(int id)
	{
		var floor = await _floorManager.GetFloorWithRoomsAsync(id);

		if (floor == null)
			return NotFound(new { message = $"Floor with ID {id} not found." });

		return Ok(new
		{
			floorId = floor.Id,
			floorNumber = floor.FloorNumber,
			roomCount = floor.Rooms.Count
		});
	}
}