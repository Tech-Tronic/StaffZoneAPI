using Microsoft.EntityFrameworkCore;
using StaffZone.Entities;
using StaffZone.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationRepositories();
builder.Services.AddApplicationManagers();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<StaffZoneContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
