namespace Project_Tests;

public class IntegrationTestsRestaurant
{
    [Fact]
    public async Task Should_Return_Restaurants()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/restaurants");
        results.StatusCode.Should().Be(HttpStatusCode.OK);
        var restaurants = await results.Content.ReadFromJsonAsync<List<Restaurant>>();
        Assert.NotNull(restaurants);
        Assert.IsType<List<Restaurant>>(restaurants);
        Assert.True(restaurants.Count > 0);
    }

    [Fact]
    public async Task Should_Return_Restaurant()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/restaurant/1");
        results.StatusCode.Should().Be(HttpStatusCode.OK);
        var restaurant = await results.Content.ReadFromJsonAsync<Restaurant>();
        Assert.NotNull(restaurant);
        Assert.IsType<Restaurant>(restaurant);
    }

    [Fact]
    public async Task Should_Not_Return_Restaurant()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/restaurant/5");
        results.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

   [Fact]
    public async Task Should_Post_Restaurant()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        Restaurant _restaurantValid = new Restaurant(){
            Name="Test",
            Email="test@gmail.com",
            Country="test",
            Province="test",
            City="test",
            StreetAndNumber="test"
            };
        var results = await client.PostAsJsonAsync("/restaurant", _restaurantValid);

        Assert.NotNull(results.Headers.Location);
        results.StatusCode.Should().Be(HttpStatusCode.Created);
        var restaurant = await results.Content.ReadFromJsonAsync<Restaurant>();
        Assert.NotNull(restaurant);
        Assert.IsType<Restaurant>(restaurant);
    }

    [Fact]
    public async Task Should_Not_Post_Restaurant1()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        Restaurant _restaurantInvalid = new Restaurant(){
            Name="Test",
            Email="test@gmail.com",
            Country="test",
            Province="test",
            City="test"
            };
        var results = await client.PostAsJsonAsync("/restaurant", _restaurantInvalid);

        results.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Not_Post_Restaurant2()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        Restaurant _restaurantInvalid = new Restaurant(){
            Name="Test",
            Email="test",
            Country="test",
            Province="test",
            City="test",
            StreetAndNumber="test"
            };
        var results = await client.PostAsJsonAsync("/restaurant", _restaurantInvalid);

        results.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}