using Microsoft.EntityFrameworkCore;

namespace StaffZone.Entities;

public class StaffZoneContext : DbContext
{
	public StaffZoneContext(DbContextOptions<StaffZoneContext> options) : base(options) { }

	// 2. The Tables
	public DbSet<Floor> Floors { get; set; }
	public DbSet<Room> Rooms { get; set; }
	public DbSet<Guest> Guests { get; set; }
	public DbSet<Booking> Bookings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Convert Enum to String in Database ("VIP" instead of 1)
		modelBuilder.Entity<Room>()
			.Property(r => r.Type)
			.HasConversion<string>();

		modelBuilder.Entity<Room>()
			.Property(r => r.State)
			.HasConversion<string>();

		// Configure One-to-Many: Floor -> Rooms
		modelBuilder.Entity<Room>()
			.HasOne(r => r.Floor)
			.WithMany(f => f.Rooms)
			.HasForeignKey(r => r.FloorId)
			.OnDelete(DeleteBehavior.Cascade); // If Floor is deleted, delete its Rooms

		modelBuilder.Entity<Booking>()
			.Property(b => b.TotalPrice)
			.HasPrecision(18, 2); // Stores up to 18 digits, 2 decimals

		// Configure One-to-Many: Guest -> Bookings
		modelBuilder.Entity<Booking>()
			.HasOne(b => b.Guest)
			.WithMany() // Guest doesn't need a "List<Booking>" property strictly but the relation exists in the database
			.HasForeignKey(b => b.GuestId);
	}

}
