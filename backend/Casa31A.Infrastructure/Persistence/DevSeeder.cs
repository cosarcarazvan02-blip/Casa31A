using Casa31A.Domain.Rooms;
using Microsoft.EntityFrameworkCore;

namespace Casa31A.Infrastructure.Persistence;

public static class DevSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.Rooms.AnyAsync(cancellationToken))
            return;

        db.Rooms.AddRange(
            new Room
            {
                Id = Guid.NewGuid(),
                Name = "Camera Dubla Vedere Munte",
                Description = "Camera dubla, confortabila, cu vedere spre munti si baie proprie.",
                Capacity = 2,
                PricePerNightRon = 250,
                Amenities = ["Wi-Fi", "TV", "Baie proprie", "Vedere munte"],
                ImageUrls = []
            },
            new Room
            {
                Id = Guid.NewGuid(),
                Name = "Camera Familiala",
                Description = "Camera spatioasa, potrivita pentru familii cu copii, pana la 4 persoane.",
                Capacity = 4,
                PricePerNightRon = 400,
                Amenities = ["Wi-Fi", "TV", "Baie proprie", "Balcon"],
                ImageUrls = []
            },
            new Room
            {
                Id = Guid.NewGuid(),
                Name = "Camera Single",
                Description = "Camera pentru o persoana, ideala pentru sejururi de relaxare in Borsec.",
                Capacity = 1,
                PricePerNightRon = 150,
                Amenities = ["Wi-Fi", "TV"],
                ImageUrls = []
            });

        await db.SaveChangesAsync(cancellationToken);
    }
}
