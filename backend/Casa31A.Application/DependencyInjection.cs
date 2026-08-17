using Casa31A.Application.Bookings;
using Casa31A.Application.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace Casa31A.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RoomsService>();
        services.AddScoped<BookingsService>();

        return services;
    }
}
