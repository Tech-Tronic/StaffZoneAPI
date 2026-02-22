using StaffZone.DTOs.Guest;
using StaffZone.Entities;

namespace StaffZone.Managers.Contracts;

public interface IGuestManager : IGenericManager<GuestDto, Guest>
{
	Task<GuestDto?> GetGuestByEmailAsync(string email);
	Task<GuestDto?> GetGuestByNameAsync(string firstName);
	Task<GuestDto?> GetGuestByPhoneNumberAsync(string phoneNumber);
	Task<GuestDto> CreateGuestAsync(CreateGuestDto createGuestDto);
	Task<bool> UpdateGuestAsync(int id, CreateGuestDto updateGuestDto);
}
