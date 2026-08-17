using Casa31A.Application.Common;
using Casa31A.Domain.Bookings;
using Casa31A.Domain.Guests;
using Casa31A.Domain.Rooms;
using Microsoft.EntityFrameworkCore;

namespace Casa31A.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(r => r.Name).HasMaxLength(200);
            entity.Property(r => r.PricePerNightRon).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.Property(g => g.FullName).HasMaxLength(200);
            entity.Property(g => g.Email).HasMaxLength(200);
            entity.Property(g => g.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.Property(b => b.TotalAmountRon).HasPrecision(10, 2);
            entity.Property(b => b.Status).HasConversion<string>().HasMaxLength(30);

            entity.HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Guest)
                .WithMany()
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
