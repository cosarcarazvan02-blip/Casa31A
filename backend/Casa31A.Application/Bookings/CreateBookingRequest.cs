namespace Casa31A.Application.Bookings;

public record CreateBookingRequest(
    Guid RoomId,
    DateOnly CheckIn,
    DateOnly CheckOut,
    int NumberOfGuests,
    string GuestFullName,
    string GuestEmail,
    string GuestPhone);

public record CreateBookingResult(Guid BookingId, string CheckoutUrl);
