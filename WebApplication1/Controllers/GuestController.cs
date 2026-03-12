using Microsoft.AspNetCore.Mvc;
using StaffZone.DTOs.Guest;
using StaffZone.Managers.Contracts;

namespace StaffZone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestController : ControllerBase
{
	private readonly IGuestManager _guestManager;

	public GuestController(IGuestManager guestManager)
		=> _guestManager = guestManager;

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GuestDto>))]
	public async Task<IActionResult> GetAllGuests()
	{
		var guests = await _guestManager.GetAllAsync();
		return Ok(guests);
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	public async Task<IActionResult> GetGuestById(int id)
	{
		var guest = await _guestManager.GetByIdAsync(id);

		if (guest == null)
			return NotFound(new { message = $"Guest with ID {id} not found." });

		return Ok(guest);
	}

	[HttpGet("email/{email}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	public async Task<IActionResult> GetGuestByEmail(string email)
	{
		var guest = await _guestManager.GetGuestByEmailAsync(email);

		if (guest == null)
			return NotFound(new { message = $"Guest with email {email} not found." });

		return Ok(guest);
	}

	[HttpGet("name/{firstName}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	public async Task<IActionResult> GetGuestByName(string firstName)
	{
		var guest = await _guestManager.GetGuestByNameAsync(firstName);

		if (guest == null)
			return NotFound(new { message = $"Guest with name {firstName} not found." });

		return Ok(guest);
	}

	[HttpGet("phone/{phoneNumber}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	public async Task<IActionResult> GetGuestByPhoneNumber(string phoneNumber)
	{
		var guest = await _guestManager.GetGuestByPhoneNumberAsync(phoneNumber);

		if (guest == null)
			return NotFound(new { message = $"Guest with phone number {phoneNumber} not found." });

		return Ok(guest);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GuestDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> CreateGuest([FromBody] CreateGuestDto createGuestDto)
	{
		try
		{
			var guest = await _guestManager.CreateGuestAsync(createGuestDto);
			return CreatedAtAction(
				nameof(GetGuestById),
				new { id = guest.Id },
				guest);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> UpdateGuest(int id, [FromBody] CreateGuestDto updateGuestDto)
	{
		try
		{
			var result = await _guestManager.UpdateGuestAsync(id, updateGuestDto);

			if (!result)
				return NotFound(new { message = $"Guest with ID {id} not found." });

			return NoContent();
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	public async Task<IActionResult> DeleteGuest(int id)
	{
		var result = await _guestManager.DeleteAsync(id);

		if (!result)
			return NotFound(new { message = $"Guest with ID {id} not found." });

		return NoContent();
	}
}
