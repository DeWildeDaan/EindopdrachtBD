namespace Project_Tests;

public class FakeRestaurantRepository : IRestaurantRepository
{
    private readonly List<Restaurant> _restaurants = new();
    public Task<List<Restaurant>> GetRestaurants(){
        _restaurants.AddRange(new List<Restaurant>() { new Restaurant() {RestaurantId = "1", Name = "Daan1", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" }, new Restaurant() {RestaurantId = "1", Name = "Daan2", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" } });
        return Task.FromResult(_restaurants);
    }

    public Task<Restaurant> GetRestaurant(string id){
        _restaurants.AddRange(new List<Restaurant>() { new Restaurant() {RestaurantId = "1", Name = "Daan1", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" }, new Restaurant() {RestaurantId = "1", Name = "Daan2", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" } });
        Restaurant restaurant = _restaurants.Find(x => x.RestaurantId == id);
        return Task.FromResult(restaurant);
    }

    public Task<Restaurant> AddRestaurant(Restaurant newRestaurant)
    {
        try
        {
            _restaurants.Add(newRestaurant);
            return Task.FromResult(newRestaurant);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<Restaurant> UpdateRestaurant(Restaurant newRestaurant)
    {
        try
        {
            _restaurants.AddRange(new List<Restaurant>() { new Restaurant() {RestaurantId = "1", Name = "Daan1", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" }, new Restaurant() {RestaurantId = "1", Name = "Daan2", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" } });
            Restaurant restaurant = _restaurants.Find(x => x.RestaurantId == newRestaurant.RestaurantId);
            _restaurants.Remove(restaurant);
            _restaurants.Append(newRestaurant);
            return Task.FromResult(newRestaurant);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<String> DeleteRestaurant(string id)
    {
        try
        {
            _restaurants.AddRange(new List<Restaurant>() { new Restaurant() {RestaurantId = "1", Name = "Daan1", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" }, new Restaurant() {RestaurantId = "1", Name = "Daan2", Email = "daan1.dewilde@gmail.com", Country = "Belgium", Province = "East-Flanders", City = "Zele", StreetAndNumber = "Hansevelde 100" } });
            Restaurant restaurant = _restaurants.Find(x => x.RestaurantId == id);
            _restaurants.Remove(restaurant);
            return Task.FromResult($"Deleted {id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}