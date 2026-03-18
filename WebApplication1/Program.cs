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


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers()
	.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StaffZoneContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

var app = builder.Build();

#region Middlewaer Pipeline

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseHttpsRedirection();
}

app.MapControllers();
app.UseRouting();

#endregion

app.MapGet("/hello", () => "Hello World!");

app.Run();
