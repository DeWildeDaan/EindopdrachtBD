namespace Project.Service;

public interface IRestaurantService
{
    Task<Restaurant> AddRestaurant(Restaurant restaurant);
    Task<List<Restaurant>> Setup();
    Task<string> DeleteRestaurant(string id);
    Task<Restaurant> GetRestaurant(string id);
    Task<List<Restaurant>> GetRestaurants();
    Task<Restaurant> UpdateRestaurant(Restaurant restaurant);
    Task UpdateRestaurantData(Review review, double avg, int count);
}

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<List<Restaurant>> Setup()
    {
        List<Restaurant> restaurants = new List<Restaurant>() { new Restaurant() { Name = "Daan1", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" }, new Restaurant() { Name = "Daan2", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" } };
        foreach (Restaurant restaurant in restaurants)
        {
            await AddRestaurant(restaurant);
        }
        return await GetRestaurants();
    }

    public async Task<List<Restaurant>> GetRestaurants()
    {
        return await _restaurantRepository.GetRestaurants();
    }

    public async Task<Restaurant> GetRestaurant(string id)
    {
        return await _restaurantRepository.GetRestaurant(id);
    }

    public async Task<Restaurant> AddRestaurant(Restaurant restaurant)
    {
        //restaurant.RestaurantId = Guid.NewGuid().ToString();
        restaurant.TotalReviews = 0;
        restaurant.AverageScore = 0;
        return await _restaurantRepository.AddRestaurant(restaurant);
    }

    public async Task<Restaurant> UpdateRestaurant(Restaurant restaurant)
    {
        return await _restaurantRepository.UpdateRestaurant(restaurant);
    }

    public async Task<String> DeleteRestaurant(string id)
    {
        return await _restaurantRepository.DeleteRestaurant(id);
    }

    public async Task UpdateRestaurantData(Review review, double avg, int count){
        Restaurant restaurant = await _restaurantRepository.GetRestaurant(review.RestaurantId);
        restaurant.AverageScore = avg;
        restaurant.TotalReviews = count;
        await _restaurantRepository.UpdateRestaurant(restaurant);
    }
}