using System.ComponentModel.DataAnnotations;

namespace Casa31A.Infrastructure.Payments;

public class StripeOptions
{
    public const string SectionName = "Stripe";

    [Required] public required string SecretKey { get; set; }
    [Required] public required string WebhookSecret { get; set; }
    [Required] public required string SuccessUrl { get; set; }
    [Required] public required string CancelUrl { get; set; }
}
