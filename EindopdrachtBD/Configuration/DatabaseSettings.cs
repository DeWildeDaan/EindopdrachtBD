namespace Project.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? RestaurantsCollection { get; set; }
    public string? ReviewsCollection { get; set; }
    public string? UsersCollection { get; set; }
    
}