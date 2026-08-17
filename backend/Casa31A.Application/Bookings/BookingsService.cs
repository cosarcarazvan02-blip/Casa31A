using Casa31A.Application.Common;
using Casa31A.Application.Payments;
using Casa31A.Application.Rooms;
using Casa31A.Domain.Bookings;
using Casa31A.Domain.Guests;
using Microsoft.EntityFrameworkCore;

namespace Casa31A.Application.Bookings;

public class BookingsService(IAppDbContext db, RoomsService roomsService, IPaymentService paymentService)
{
    public async Task<CreateBookingResult> CreateBookingAsync(CreateBookingRequest request, CancellationToken cancellationToken = default)
    {
        if (request.CheckIn >= request.CheckOut)
            throw new BookingValidationException("Check-out date must be after check-in date.");

        if (request.CheckIn < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new BookingValidationException("Check-in date cannot be in the past.");

        var room = await db.Rooms.FirstOrDefaultAsync(r => r.Id == request.RoomId && r.IsActive, cancellationToken)
            ?? throw new BookingValidationException("Room not found.");

        if (request.NumberOfGuests < 1 || request.NumberOfGuests > room.Capacity)
            throw new BookingValidationException($"Number of guests must be between 1 and {room.Capacity}.");

        var isAvailable = await roomsService.IsAvailableAsync(request.RoomId, request.CheckIn, request.CheckOut, cancellationToken);
        if (!isAvailable)
            throw new BookingValidationException("Room is not available for the selected dates.");

        var guest = new Guest
        {
            Id = Guid.NewGuid(),
            FullName = request.GuestFullName,
            Email = request.GuestEmail,
            Phone = request.GuestPhone
        };
        db.Guests.Add(guest);

        var nights = request.CheckOut.DayNumber - request.CheckIn.DayNumber;
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = room.Id,
            GuestId = guest.Id,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            NumberOfGuests = request.NumberOfGuests,
            TotalAmountRon = nights * room.PricePerNightRon,
            Status = BookingStatus.PendingPayment
        };
        db.Bookings.Add(booking);

        var checkoutSession = await paymentService.CreateCheckoutSessionAsync(booking, cancellationToken);
        booking.StripeSessionId = checkoutSession.SessionId;

        await db.SaveChangesAsync(cancellationToken);

        return new CreateBookingResult(booking.Id, checkoutSession.CheckoutUrl);
    }

    public async Task MarkPaidAsync(string stripeSessionId, CancellationToken cancellationToken = default)
    {
        var booking = await db.Bookings.FirstOrDefaultAsync(b => b.StripeSessionId == stripeSessionId, cancellationToken);
        if (booking is null || booking.Status == BookingStatus.Paid)
            return;

        booking.Status = BookingStatus.Paid;
        await db.SaveChangesAsync(cancellationToken);
    }
}
