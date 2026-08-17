using Casa31A.Domain.Bookings;

namespace Casa31A.Application.Payments;

public record CheckoutSession(string SessionId, string CheckoutUrl);

public interface IPaymentService
{
    Task<CheckoutSession> CreateCheckoutSessionAsync(Booking booking, CancellationToken cancellationToken = default);

    /// <summary>Validates a Stripe webhook payload/signature and returns the paid session id, or null if the event should be ignored.</summary>
    string? TryGetPaidSessionId(string payload, string signatureHeader);
}
