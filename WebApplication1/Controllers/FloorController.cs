using Microsoft.AspNetCore.Mvc;
using StaffZone.Repos.Contracts;
using StaffZone.Entities;

namespace StaffZone.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FloorController : ControllerBase
	{
		private IFloorRepository _floorRepository;
		public FloorController(IFloorRepository floorRepository)
		{
			_floorRepository = floorRepository;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateFloor()
		{
			_floorRepository.AddAsync(new() { FloorNumber = 100 });
			return Ok();
		}

		[HttpGet]
		public async Task<IEnumerable<Floor>> GetAllFloors()
		{
			var a = await _floorRepository.GetAllAsync();
			return a;
		}

		[HttpPut]
		public async Task<IActionResult> UpdateFloor(int floorId, int newFloorNumber)
		{
			await _floorRepository.UpdateAsync(floorId, new() { Id = floorId, FloorNumber = newFloorNumber });
			return Ok();
		}
	}
}
