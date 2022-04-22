namespace Project.Model;

public class Review
{
    public string? ReviewId { get; set; }
    public string? RestaurantId { get; set; }
    public string? Text { get; set; }
    public DateTime DateAndTime { get; set; }
}