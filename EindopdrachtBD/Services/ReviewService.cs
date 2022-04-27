namespace Project.Service;

public interface IReviewService
{
    Task<Review> AddReview(Review review);
    Task<string> DeleteReview(string id);
    Task<Review> GetReview(string id);
    Task<List<Review>> GetReviews();
    Task<Review> UpdateReview(Review review);
}

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
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
        review.ReviewId = Guid.NewGuid().ToString();
        review.DateAndTime = DateTime.Now;
        return await _reviewRepository.AddReview(review);
    }

    public async Task<Review> UpdateReview(Review review)
    {
        return await _reviewRepository.UpdateReview(review);
    }

    public async Task<String> DeleteReview(string id)
    {
        return await _reviewRepository.DeleteReview(id);
    }

}