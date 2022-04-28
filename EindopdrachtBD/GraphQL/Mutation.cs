namespace Project.GraphQL;

public class Mutation
{
    public async Task<RestaurantPayload> AddRestaurant([Service] IRestaurantService restaurantService, AddRestaurantInput input)
    {
        var newRestaurant = new Restaurant()
        {
            Name = input.name,
            Email = input.email,
            Country = input.country,
            Province = input.province,
            City = input.city,
            StreetAndNumber = input.streetandnumber
        };
        var created =  await restaurantService.AddRestaurant(newRestaurant);
        return new RestaurantPayload(created);
    }

    public async Task<RestaurantPayload> UpdateRestaurant([Service] IRestaurantService restaurantService, UpdateRestaurantInput input)
    {
        var newRestaurant = new Restaurant()
        {
            Name = input.name,
            Email = input.email,
            Country = input.country,
            Province = input.province,
            City = input.city,
            StreetAndNumber = input.streetandnumber,
            TotalReviews = input.totalreviews,
            AverageScore = input.averagescore
        };
        var updated =  await restaurantService.UpdateRestaurant(newRestaurant);
        return new RestaurantPayload(updated);
    }

    public async Task<String> DeleteRestaurant([Service] IRestaurantService restaurantService, DeleteRestaurantInput input)
    {
        return await restaurantService.DeleteRestaurant(input.restaurantid);
    }

    public async Task<ReviewPayload> AddReview([Service] IReviewService reviewService, AddReviewInput input)
    {
        var newReview = new Review()
        {
            RestaurantId = input.restaurantid,
            Text = input.text,
            Score = input.score
        };
        var created =  await reviewService.AddReview(newReview);
        return new ReviewPayload(created);
    }

    public async Task<ReviewPayload> UpdateReview([Service] IReviewService reviewService, UpdateReviewInput input)
    {
        var newReview = new Review()
        {
            ReviewId = input.reviewid,
            Text = input.text,
            Score = input.score
        };
        var updated =  await reviewService.UpdateReview(newReview);
        return new ReviewPayload(updated);
    }

    public async Task<String> DeleteReview([Service] IRestaurantService restaurantService, DeleteReviewInput input)
    {
        return await restaurantService.DeleteRestaurant(input.reviewid);
    }
}