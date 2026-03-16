using Microsoft.AspNetCore.Mvc;
using StaffZone.DTOs.Booking;
using StaffZone.Managers.Contracts;
using StaffZone.Helpers;

namespace StaffZone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
	private readonly IBookingManager _bookingManager;

	public BookingController(IBookingManager bookingManager)
		=> _bookingManager = bookingManager;

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookingDto>))]
	public async Task<IActionResult> GetAllBookings()
	{
		var bookings = await _bookingManager.GetAllAsync();
		return Ok(bookings);
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> GetBookingById(int id)
	{
		if (!Validator.IsValidId(id))
			return BadRequest(new { message = "Invalid booking ID. ID must be a positive number." });

		var booking = await _bookingManager.GetByIdAsync(id);

		if (booking == null)
			return NotFound(new { message = $"Booking with ID {id} not found." });

		return Ok(booking);
	}

	[HttpGet("{id}/details")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingDetailsDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> GetBookingDetails(int id)
	{
		if (!Validator.IsValidId(id))
			return BadRequest(new { message = "Invalid booking ID. ID must be a positive number." });

		var bookingDetails = await _bookingManager.GetBookingDetailsAsync(id);

		if (bookingDetails == null)
			return NotFound(new { message = $"Booking with ID {id} not found." });

		return Ok(bookingDetails);
	}

	[HttpGet("guest/{guestId}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookingDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> GetGuestBookings(int guestId)
	{
		if (!Validator.IsValidId(guestId))
			return BadRequest(new { message = "Invalid guest ID. ID must be a positive number." });

		var bookings = await _bookingManager.GetGuestBookingsAsync(guestId);
		return Ok(bookings);
	}

	[HttpGet("active")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookingDto>))]
	public async Task<IActionResult> GetActiveBookings()
	{
		var bookings = await _bookingManager.GetActiveBookingsAsync();
		return Ok(bookings);
	}

	[HttpGet("check-availability")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> CheckAvailability(
		[FromQuery] int roomId,
		[FromQuery] DateTime checkIn,
		[FromQuery] DateTime checkOut)
	{
		if (!Validator.IsValidId(roomId))
			return BadRequest(new { message = "Invalid room ID. ID must be a positive number." });

		var isAvailable = await _bookingManager.CheckAvailabilityAsync(roomId, checkIn, checkOut);
		return Ok(new { roomId, checkIn, checkOut, isAvailable });
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookingDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createBookingDto)
	{
		try
		{
			var booking = await _bookingManager.CreateBookingAsync(createBookingDto);
			return CreatedAtAction(
				nameof(GetBookingById),
				new { id = booking.Id },
				booking);
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

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
	public async Task<IActionResult> DeleteBooking(int id)
	{
		if (!Validator.IsValidId(id))
			return BadRequest(new { message = "Invalid booking ID. ID must be a positive number." });

		var result = await _bookingManager.CancelBookingAsync(id);

		if (!result)
			return NotFound(new { message = $"Booking with ID {id} not found." });

		return NoContent();
	}
}
