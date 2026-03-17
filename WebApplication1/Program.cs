using Microsoft.EntityFrameworkCore;
using StaffZone.Entities;
using StaffZone.Extensions;
using StaffZone.Mappings;
using StaffZone.Middlewares;
using System.Text.Json.Serialization;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationRepositories();
builder.Services.AddApplicationManagers();

new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
	.AssertConfigurationIsValid();

// AutoMapper - Singleton (configured once)
// IMapper interface that you inject into your controllers or services is registered as Transient by default.
builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
{
	// This makes all enums responses serialize as text globally.
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<StaffZoneContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

/*builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.DisableImplicitFromServicesParameters = true;
});*/

//Add your custom service
//builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddSingleton<ICacheService, RedisCacheService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		policy.WithOrigins("http://localhost:3000")  // Your frontend URL
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();

// Add global exception handling middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	// Only redirect to HTTPS in production
	app.UseHttpsRedirection();
}

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();


app.Run();
