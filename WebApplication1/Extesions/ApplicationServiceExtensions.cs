using StaffZone.Repos.Repositories;
using StaffZone.Repos.Contracts;
using StaffZone.Managers.Contracts;
using StaffZone.Managers.Implementations;

namespace StaffZone.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
	{
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IFloorRepository, FloorRepository>();
		services.AddScoped<IRoomRepository, RoomRepository>();
		services.AddScoped<IGuestRepository, GuestRepository>();
		services.AddScoped<IBookingRepository, BookingRepository>();
		return services;
	}

	public static IServiceCollection AddApplicationManagers(this IServiceCollection services)
	{
		services.AddScoped<IFloorManager, FloorManager>();
		services.AddScoped<IRoomManager, RoomManager>();
		services.AddScoped<IGuestManager, GuestManager>();
		services.AddScoped<IBookingManager, BookingManager>();
		return services;
	}
}
