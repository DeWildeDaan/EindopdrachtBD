namespace Project_Tests;

public class FakeReviewRepository : IReviewRepository
{
    private readonly List<Review> _reviews = new();
    public Task<List<Review>> GetReviews(){
        _reviews.AddRange(new List<Review>() {new Review() {ReviewId = "1", RestaurantId = "1", Text = "test", DateAndTime = DateTime.Now}, new Review() {ReviewId = "2", RestaurantId = "1", Text = "test2", DateAndTime = DateTime.Now}});
        return Task.FromResult(_reviews);
    }

    public Task<List<Review>> GetReviewsRestaurant(string id){
        _reviews.AddRange(new List<Review>() {new Review() {ReviewId = "1", RestaurantId = "1", Text = "test", DateAndTime = DateTime.Now}, new Review() {ReviewId = "2", RestaurantId = "1", Text = "test2", DateAndTime = DateTime.Now}});
        List<Review> reviews = _reviews.FindAll(x => x.RestaurantId == id);
        return Task.FromResult(reviews);
    }

    public Task<Review> GetReview(string id){
        _reviews.AddRange(new List<Review>() {new Review() {ReviewId = "1", RestaurantId = "1", Text = "test", DateAndTime = DateTime.Now}, new Review() {ReviewId = "2", RestaurantId = "1", Text = "test2", DateAndTime = DateTime.Now}});
        Review review = _reviews.Find(x => x.ReviewId == id);
        return Task.FromResult(review);
    }

    public Task<Review> AddReview(Review newReview)
    {
        try
        {
            _reviews.Add(newReview);
            return Task.FromResult(newReview);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<Review> UpdateReview(Review newReview)
    {
        try
        {
            _reviews.AddRange(new List<Review>() {new Review() {ReviewId = "1", RestaurantId = "1", Text = "test", DateAndTime = DateTime.Now}, new Review() {ReviewId = "2", RestaurantId = "1", Text = "test2", DateAndTime = DateTime.Now}});
            Review review = _reviews.Find(x => x.ReviewId == newReview.ReviewId);
            _reviews.Remove(review);
            _reviews.Append(newReview);
            return Task.FromResult(newReview);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<String> DeleteReview(string id)
    {
        try
        {
            _reviews.AddRange(new List<Review>() {new Review() {ReviewId = "1", RestaurantId = "1", Text = "test", DateAndTime = DateTime.Now}, new Review() {ReviewId = "2", RestaurantId = "1", Text = "test2", DateAndTime = DateTime.Now}});
            Review review = _reviews.Find(x => x.ReviewId == id);
            _reviews.Remove(review);
            return Task.FromResult($"Deleted {id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}