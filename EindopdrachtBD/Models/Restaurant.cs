namespace Project.Model;

public class Restaurant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? RestaurantId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Country { get; set; }
    public string? Province { get; set; }
    public string? City { get; set; }
    public string? StreetAndNumber { get; set; }
    public int TotalReviews { get; set; }
    public double AverageScore { get; set; }
}