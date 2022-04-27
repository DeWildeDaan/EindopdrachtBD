namespace Project.Repositories;

public interface IRestaurantRepository
{
    Task<Restaurant> AddRestaurant(Restaurant newRestaurant);
    Task<string> DeleteRestaurant(string id);
    Task<Restaurant> GetRestaurant(string id);
    Task<List<Restaurant>> GetRestaurants();
    Task<Restaurant> UpdateRestaurant(Restaurant newRestaurant);
}

public class RestaurantRepository : IRestaurantRepository
{
    private readonly IMongoContext _context;
    public RestaurantRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Restaurant>> GetRestaurants() => await _context.RestaurantsCollection.Find<Restaurant>(_ => true).ToListAsync();

    public async Task<Restaurant> GetRestaurant(string id) => await _context.RestaurantsCollection.Find<Restaurant>(x => x.RestaurantId == id).FirstOrDefaultAsync();

    public async Task<Restaurant> AddRestaurant(Restaurant newRestaurant)
    {
        try
        {
            await _context.RestaurantsCollection.InsertOneAsync(newRestaurant);
            return newRestaurant;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Restaurant> UpdateRestaurant(Restaurant newRestaurant)
    {
        try
        {
            var filter = Builders<Restaurant>.Filter.Eq("RestaurantId", newRestaurant.RestaurantId);
            var update = Builders<Restaurant>.Update.Set("Name", newRestaurant.Name).Set("Email", newRestaurant.Email).Set("Country", newRestaurant.Country).Set("Province", newRestaurant.Province).Set("City", newRestaurant.City).Set("StreetAndNumber", newRestaurant.StreetAndNumber);
            var result = await _context.RestaurantsCollection.UpdateOneAsync(filter, update);
            return await GetRestaurant(newRestaurant.RestaurantId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<String> DeleteRestaurant(string id)
    {
        try
        {
            var filter = Builders<Restaurant>.Filter.Eq("RestaurantId", id);
            var result = await _context.RestaurantsCollection.DeleteOneAsync(filter);
            return $"Deleted {id}";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
