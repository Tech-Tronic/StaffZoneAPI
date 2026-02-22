using AutoMapper;
using StaffZone.DTOs.Room;
using StaffZone.Entities;
using StaffZone.DTOs.Guest;
using StaffZone.DTOs.Floor;
using StaffZone.DTOs.Booking;

namespace StaffZone.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// Room mappings
		CreateMap<Room, RoomDto>();
		CreateMap<CreateRoomDto, Room>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.State, opt => opt.Ignore())
			.ForMember(dest => dest.Floor, opt => opt.Ignore());
		CreateMap<UpdateRoomDto, Room>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.Floor, opt => opt.Ignore());

		// Guest mappings
		CreateMap<Guest, GuestDto>();
		CreateMap<CreateGuestDto, Guest>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.VisitCount, opt => opt.MapFrom(src => 0));

		// Floor mappings
		CreateMap<Floor, FloorDto>();
		CreateMap<Floor, FloorWithRoomsDto>();

		// Booking mappings
		CreateMap<Booking, BookingDto>();
		CreateMap<CreateBookingDto, Booking>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
			.ForMember(dest => dest.Room, opt => opt.Ignore())
			.ForMember(dest => dest.Guest, opt => opt.Ignore());
		
		CreateMap<Booking, BookingDetailsDto>()
			.ForMember(dest => dest.NumberOfNights, 
				opt => opt.MapFrom(src => (src.CheckOutDate - src.CheckInDate).Days));
	}
}
