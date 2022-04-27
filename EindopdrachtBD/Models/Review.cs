namespace Project.Model;

public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ReviewId { get; set; }
    public string? RestaurantId { get; set; }
    public string? Text { get; set; }
    public int Score { get; set; }
    public DateTime DateAndTime { get; set; }
}