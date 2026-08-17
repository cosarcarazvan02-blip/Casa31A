using Casa31A.Application.Payments;
using Casa31A.Domain.Bookings;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Casa31A.Infrastructure.Payments;

public class StripePaymentService(IOptions<StripeOptions> options) : IPaymentService
{
    private readonly StripeOptions _options = options.Value;

    public async Task<CheckoutSession> CreateCheckoutSessionAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        var service = new SessionService();
        var sessionOptions = new SessionCreateOptions
        {
            Mode = "payment",
            PaymentMethodTypes = ["card"],
            SuccessUrl = $"{_options.SuccessUrl}?bookingId={booking.Id}",
            CancelUrl = $"{_options.CancelUrl}?bookingId={booking.Id}",
            ClientReferenceId = booking.Id.ToString(),
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "ron",
                        UnitAmount = (long)(booking.TotalAmountRon * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Casa31A - rezervare {booking.CheckIn:dd.MM.yyyy} - {booking.CheckOut:dd.MM.yyyy}"
                        }
                    }
                }
            ]
        };

        var session = await service.CreateAsync(sessionOptions, cancellationToken: cancellationToken);
        return new CheckoutSession(session.Id, session.Url);
    }

    public string? TryGetPaidSessionId(string payload, string signatureHeader)
    {
        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(payload, signatureHeader, _options.WebhookSecret);
        }
        catch (StripeException)
        {
            return null;
        }

        if (stripeEvent.Type != EventTypes.CheckoutSessionCompleted)
            return null;

        var session = stripeEvent.Data.Object as Session;
        return session?.PaymentStatus == "paid" ? session.Id : null;
    }
}
