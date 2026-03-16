using AutoMapper;
using StaffZone.DTOs.Guest;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;
using StaffZone.Helpers;

namespace StaffZone.Managers.Implementations;

public class GuestManager : GenericManager<GuestDto, Guest>, IGuestManager
{
	private readonly IGuestRepository _guestRepository;

	public GuestManager(IGuestRepository guestRepository, IMapper mapper)
		: base(guestRepository, mapper)
	{
		_guestRepository = guestRepository;
	}

	public async Task<GuestDto?> GetGuestByEmailAsync(string email)
	{
		var guest = await _guestRepository.GetGuestByEmailAsync(email);
		return _mapper.Map<GuestDto?>(guest);
	}

	public async Task<GuestDto?> GetGuestByNameAsync(string firstName)
	{
		var guest = await _guestRepository.GetGuestByNameAsync(firstName);
		return _mapper.Map<GuestDto?>(guest);
	}

	public async Task<GuestDto?> GetGuestByPhoneNumberAsync(string phoneNumber)
	{
		var guest = await _guestRepository.GetGuestByPhoneNumberAsync(phoneNumber);
		return _mapper.Map<GuestDto?>(guest);
	}

	public async Task<GuestDto> CreateGuestAsync(CreateGuestDto createGuestDto)
	{
		if (Validator.HasNullInfo(createGuestDto.PhoneNumber, createGuestDto.Email))
			throw new ArgumentException("Guest information are required.");
		
		if (await ExistingInfo(createGuestDto.PhoneNumber, createGuestDto.Email))
			throw new InvalidOperationException($"Guest with these info already exists.");

		var guest = _mapper.Map<Guest>(createGuestDto);

		await _guestRepository.AddAsync(guest);
		return _mapper.Map<GuestDto>(guest);
	}

	public async Task<bool> UpdateGuestAsync(int id, CreateGuestDto updateGuestDto)
	{
		var existingGuest = await _guestRepository.GetByIdAsync(id);
		if (existingGuest == null)
			return false;

		if (Validator.HasNullInfo(updateGuestDto.PhoneNumber, updateGuestDto.Email))
			throw new ArgumentException("Guest information are required.");

		if (await ExistingInfo(updateGuestDto.PhoneNumber, updateGuestDto.Email))
			throw new InvalidOperationException($"Guest with these info already exists.");

		var updatedGuest = _mapper.Map<Guest>(updateGuestDto);
		updatedGuest.Id = id;
		updatedGuest.VisitCount = existingGuest.VisitCount;

		await _guestRepository.UpdateAsync(id, updatedGuest);
		return true;
	}

	private async Task<bool> ExistingInfo(string? phoneNumber, string? email)
	{
		var existingPhoneNumber = await _guestRepository.GetGuestByPhoneNumberAsync(phoneNumber);
		if (existingPhoneNumber != null)
			return true;

		var existingEmail = await _guestRepository.GetGuestByEmailAsync(email);
		if (existingEmail != null)
			return true;
		
		return false;
	}
}
