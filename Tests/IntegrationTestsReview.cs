namespace Project_Tests;

public class IntegrationTestsReview
{
    [Fact]
    public async Task Should_Return_Reviews()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/reviews");
        results.StatusCode.Should().Be(HttpStatusCode.OK);
        var reviews = await results.Content.ReadFromJsonAsync<List<Review>>();
        Assert.NotNull(reviews);
        Assert.IsType<List<Review>>(reviews);
        Assert.True(reviews.Count > 0);
    }

    [Fact]
    public async Task Should_Return_Review()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/review/1");
        results.StatusCode.Should().Be(HttpStatusCode.OK);
        var reviews = await results.Content.ReadFromJsonAsync<Review>();
        Assert.NotNull(reviews);
        Assert.IsType<Review>(reviews);
    }

    [Fact]
    public async Task Should_Not_Return_Review()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/review/5");
        results.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_Return_Reviews_From_Restaurant()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/restaurantreview/1");
        results.StatusCode.Should().Be(HttpStatusCode.OK);
        var reviews = await results.Content.ReadFromJsonAsync<List<Review>>();
        Assert.NotNull(reviews);
        Assert.IsType<List<Review>>(reviews);
    }

    [Fact]
    public async Task Should_Not_Return_Reviews_From_Restaurant()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.GetAsync("/restaurantreview/5");
        results.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_Post_Review()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        Review _reviewValid = new Review(){
            ReviewId="1",
            RestaurantId="1",
            Text="test",
            Score=3
            };
        var results = await client.PostAsJsonAsync("/review", _reviewValid);

        Assert.NotNull(results.Headers.Location);
        results.StatusCode.Should().Be(HttpStatusCode.Created);
        var review = await results.Content.ReadFromJsonAsync<Review>();
        Assert.NotNull(review);
        Assert.IsType<Review>(review);
        Assert.True(review.Score == 3);
    }

    [Fact]
    public async Task Should_Not_Post_Review()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        Review _reviewValid = new Review(){
            ReviewId="1",
            RestaurantId="1"
            };
        var results = await client.PostAsJsonAsync("/review", _reviewValid);

        results.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Put_Review()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        Review _reviewValidUpdate = new Review(){
            ReviewId="1",
            RestaurantId="1",
            Text="test",
            Score=4
            };
        var results = await client.PutAsJsonAsync("/review", _reviewValidUpdate);
        Assert.NotNull(results.Headers.Location);
        results.StatusCode.Should().Be(HttpStatusCode.Created);
        var review = await results.Content.ReadFromJsonAsync<Review>();
        Assert.NotNull(review);
        Assert.IsType<Review>(review);
    }

    [Fact]
    public async Task Should_Not_Put_Review()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        Review _reviewValidUpdate = new Review(){
            ReviewId="1",
            Text="test",
            Score=4
            };
        var results = await client.PutAsJsonAsync("/review", _reviewValidUpdate);
        results.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Delete_Review()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();

        var results = await client.DeleteAsync("/review/1");
        results.StatusCode.Should().Be(HttpStatusCode.OK);
        var response = await results.Content.ReadFromJsonAsync<String>();
        Assert.NotNull(response);
        Assert.IsType<String>(response);
    }
}