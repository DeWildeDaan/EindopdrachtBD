namespace Project.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Restaurant> RestaurantsCollection { get; }
    IMongoCollection<Review> ReviewsCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Restaurant> RestaurantsCollection
    {
        get
        {
            return _database.GetCollection<Restaurant>(_settings.RestaurantsCollection);
        }
    }

    public IMongoCollection<Review> ReviewsCollection
    {
        get
        {
            return _database.GetCollection<Review>(_settings.ReviewsCollection);
        }
    }
}