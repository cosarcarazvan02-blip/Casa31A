using Casa31A.Domain.Guests;
using Casa31A.Domain.Rooms;

namespace Casa31A.Domain.Bookings;

public class Booking
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Room? Room { get; set; }
    public Guid GuestId { get; set; }
    public Guest? Guest { get; set; }
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal TotalAmountRon { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.PendingPayment;
    public string? StripeSessionId { get; set; }
    public string? StripePaymentIntentId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public int Nights => CheckOut.DayNumber - CheckIn.DayNumber;
}
