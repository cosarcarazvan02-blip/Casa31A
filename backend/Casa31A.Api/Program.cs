using Casa31A.Application;
using Casa31A.Application.Bookings;
using Casa31A.Application.Rooms;
using Casa31A.Infrastructure;
using Casa31A.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var frontendBaseUrl = builder.Configuration["Frontend:BaseUrl"]
    ?? throw new InvalidOperationException("Frontend:BaseUrl is not configured.");

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(frontendBaseUrl)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await DevSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();
app.UseCors("Frontend");

var rooms = app.MapGroup("/api/rooms");

rooms.MapGet("/", async (RoomsService roomsService, CancellationToken ct) =>
    Results.Ok(await roomsService.GetActiveRoomsAsync(ct)));

rooms.MapGet("/{roomId:guid}/availability", async (Guid roomId, DateOnly checkIn, DateOnly checkOut, RoomsService roomsService, CancellationToken ct) =>
    Results.Ok(new { available = await roomsService.IsAvailableAsync(roomId, checkIn, checkOut, ct) }));

app.MapPost("/api/bookings", async (CreateBookingRequest request, BookingsService bookingsService, CancellationToken ct) =>
{
    try
    {
        var result = await bookingsService.CreateBookingAsync(request, ct);
        return Results.Ok(result);
    }
    catch (BookingValidationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapPost("/api/stripe/webhook", async (HttpRequest httpRequest, Casa31A.Application.Payments.IPaymentService paymentService, BookingsService bookingsService, CancellationToken ct) =>
{
    using var reader = new StreamReader(httpRequest.Body);
    var payload = await reader.ReadToEndAsync(ct);
    var signature = httpRequest.Headers["Stripe-Signature"].ToString();

    var paidSessionId = paymentService.TryGetPaidSessionId(payload, signature);
    if (paidSessionId is not null)
        await bookingsService.MarkPaidAsync(paidSessionId, ct);

    return Results.Ok();
});

app.Run();
