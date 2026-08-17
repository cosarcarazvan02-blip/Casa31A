using Casa31A.Application.Common;
using Casa31A.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Casa31A.Application.Rooms;

public class RoomsService(IAppDbContext db)
{
    public async Task<List<RoomDto>> GetActiveRoomsAsync(CancellationToken cancellationToken = default)
    {
        return await db.Rooms
            .Where(r => r.IsActive)
            .Select(r => new RoomDto(r.Id, r.Name, r.Description, r.Capacity, r.PricePerNightRon, r.Amenities, r.ImageUrls))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsAvailableAsync(Guid roomId, DateOnly checkIn, DateOnly checkOut, CancellationToken cancellationToken = default)
    {
        var overlapping = await db.Bookings
            .Where(b => b.RoomId == roomId)
            .Where(b => b.Status == BookingStatus.Paid || b.Status == BookingStatus.PendingPayment)
            .Where(b => b.CheckIn < checkOut && checkIn < b.CheckOut)
            .AnyAsync(cancellationToken);

        return !overlapping;
    }
}
