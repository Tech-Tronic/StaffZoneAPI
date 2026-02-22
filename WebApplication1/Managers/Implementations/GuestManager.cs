using AutoMapper;
using StaffZone.DTOs.Guest;
using StaffZone.Entities;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;

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
		if (string.IsNullOrWhiteSpace(createGuestDto.Email))
			throw new ArgumentException("Email is required.");

		var existingGuest = await _guestRepository.GetGuestByEmailAsync(createGuestDto.Email);
		if (existingGuest != null)
			throw new InvalidOperationException($"Guest with email {createGuestDto.Email} already exists.");

		var guest = _mapper.Map<Guest>(createGuestDto);

		await _guestRepository.AddAsync(guest);
		return _mapper.Map<GuestDto>(guest);
	}

	public async Task<bool> UpdateGuestAsync(int id, CreateGuestDto updateGuestDto)
	{
		var existingGuest = await _guestRepository.GetByIdAsync(id);
		if (existingGuest == null)
			return false;

		if (string.IsNullOrWhiteSpace(updateGuestDto.Email))
			throw new ArgumentException("Email is required.");

		var updatedGuest = _mapper.Map<Guest>(updateGuestDto);
		updatedGuest.Id = id;
		updatedGuest.VisitCount = existingGuest.VisitCount; // Preserve visit count

		await _guestRepository.UpdateAsync(id, updatedGuest);
		return true;
	}
}
