namespace Casa31A.Application.Rooms;

public record RoomDto(
    Guid Id,
    string Name,
    string Description,
    int Capacity,
    decimal PricePerNightRon,
    List<string> Amenities,
    List<string> ImageUrls);
