namespace Project_Tests;

public class UnitTestsReview
{
    [Fact]
    public void Add_Review_Should_Throw_ArgumentException()
    {
        var reviewservice  = Helper.CreateReviewService();

        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.AddReview(null));

    }

    [Fact]
    public void Update_Review_Should_Throw_ArgumentException()
    {
        var reviewservice  = Helper.CreateReviewService();

        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.UpdateReview(null));

    }

    [Fact]
    public void Delete_Review_Should_Throw_ArgumentException()
    {
        var reviewservice  = Helper.CreateReviewService();

        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.DeleteReview(null));

    }

    [Fact]
    public void UpdateRestaurant_Review_Should_Throw_ArgumentException()
    {
        var reviewservice  = Helper.CreateReviewService();

        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.UpdateRestaurantData(null));

    }

    [Fact]
    public void Add_Review_Should_Throw_ArgumentException_On_No_Restaurant()
    {
        var reviewservice  = Helper.CreateReviewService();
        Review review = new Review() {ReviewId = "1", RestaurantId = "5", Text = "test", DateAndTime = DateTime.Now};
        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.AddReview(review));

    }

    [Fact]
    public void Update_Review_Should_Throw_ArgumentException_On_No_Restaurant()
    {
        var reviewservice  = Helper.CreateReviewService();
        Review review = new Review() {ReviewId = "1", RestaurantId = "5", Text = "test", DateAndTime = DateTime.Now};
        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.UpdateReview(review));

    }

    [Fact]
    public void Delete_Review_Should_Throw_ArgumentException_On_No_Restaurant()
    {
        var reviewservice  = Helper.CreateReviewService();
        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.DeleteReview("5"));

    }

    [Fact]
    public void UpdateRestaurant_Review_Should_Throw_ArgumentException_On_No_Restaurant()
    {
        var reviewservice  = Helper.CreateReviewService();
        Review review = new Review() {ReviewId = "1", RestaurantId = "5", Text = "test", DateAndTime = DateTime.Now};
        Assert.ThrowsAsync<ArgumentException>(async () => reviewservice.UpdateRestaurantData(review));

    }
    
}