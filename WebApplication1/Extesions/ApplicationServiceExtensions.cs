using StaffZone.Repos.Repositories;
using StaffZone.Repos.Contracts;

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
}
