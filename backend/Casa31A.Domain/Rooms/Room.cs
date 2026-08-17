namespace Casa31A.Domain.Rooms;

public class Room
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNightRon { get; set; }
    public List<string> Amenities { get; set; } = [];
    public List<string> ImageUrls { get; set; } = [];
    public bool IsActive { get; set; } = true;
}
