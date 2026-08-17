namespace Casa31A.Domain.Guests;

public class Guest
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}
