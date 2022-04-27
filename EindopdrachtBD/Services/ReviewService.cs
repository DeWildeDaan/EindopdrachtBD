namespace Project.Service;

public interface IReviewService
{
    Task<Review> AddReview(Review review);
    Task<string> DeleteReview(string id);
    Task<Review> GetReview(string id);
    Task<List<Review>> GetReviews();
    Task<Review> UpdateReview(Review review);
    Task GetRestaurantData(Review review);
}

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IRestaurantService _restaurantService;

    public ReviewService(IReviewRepository reviewRepository, IRestaurantService restaurantService)
    {
        _reviewRepository = reviewRepository;
        _restaurantService = restaurantService;
    }


    public async Task<List<Review>> GetReviews()
    {
        return await _reviewRepository.GetReviews();
    }

    public async Task<Review> GetReview(string id)
    {
        return await _reviewRepository.GetReview(id);
    }

    public async Task<Review> AddReview(Review review)
    {
        //review.ReviewId = Guid.NewGuid().ToString();
        review.DateAndTime = DateTime.Now;
        var results = await _reviewRepository.AddReview(review);
        await GetRestaurantData(review);
        return results;
    }

    public async Task<Review> UpdateReview(Review review)
    {
        var results = await _reviewRepository.UpdateReview(review);
        await GetRestaurantData(review);
        return results;
    }

    public async Task<String> DeleteReview(string id)
    {
        Review review = await GetReview(id);
        var results = await _reviewRepository.DeleteReview(id);
        await GetRestaurantData(review);
        return results;
    }

    public async Task GetRestaurantData(Review review){
        List<Review> reviews = await _reviewRepository.GetReviewsRestaurant(review.RestaurantId);
        int score = 0;
        int count = 0;
        foreach(Review r in reviews){
            score = score + r.Score;
            count++;
        }
        double avg = score / count;
        await _restaurantService.UpdateRestaurantData(review, Math.Round(avg, 2), count);
    }
}