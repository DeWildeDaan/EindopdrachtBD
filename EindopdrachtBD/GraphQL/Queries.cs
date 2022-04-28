namespace Project.GraphQL;

public class Queries{
    public async Task<List<Restaurant>> GetRestaurants([Service] IRestaurantService restaurantService) => await restaurantService.GetRestaurants();
    public async Task<Restaurant> GetRestaurant([Service] IRestaurantService restaurantService, string restaurantid) => await restaurantService.GetRestaurant(restaurantid);
    public async Task<List<Review>> GetReviews([Service] IReviewService reviewService) => await reviewService.GetReviews();
    public async Task<Review> GetReview([Service] IReviewService reviewService, string reviewtid) => await reviewService.GetReview(reviewtid);
    public async Task<List<Review>> GetReviewsByRestaurant([Service] IReviewService reviewService, string restaurantid) => await reviewService.GetReviewsRestaurant(restaurantid);
}