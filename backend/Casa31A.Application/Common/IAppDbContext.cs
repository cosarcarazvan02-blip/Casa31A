using Casa31A.Domain.Bookings;
using Casa31A.Domain.Guests;
using Casa31A.Domain.Rooms;
using Microsoft.EntityFrameworkCore;

namespace Casa31A.Application.Common;

public interface IAppDbContext
{
    DbSet<Room> Rooms { get; }
    DbSet<Guest> Guests { get; }
    DbSet<Booking> Bookings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
