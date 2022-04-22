namespace Project.Repositories;

public interface IReviewRepository
{
    Task<Review> AddReview(Review newReview);
    Task<string> DeleteReview(string id);
    Task<Review> GetReview(string id);
    Task<List<Review>> GetReviews();
    Task<Review> UpdateReview(Review newReview);
}

public class ReviewRepository : IReviewRepository
{
    private readonly IMongoContext _context;
    public ReviewRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetReviews() => await _context.ReviewsCollection.Find<Review>(_ => true).ToListAsync();

    public async Task<Review> GetReview(string id) => await _context.ReviewsCollection.Find<Review>(x => x.ReviewId == id).FirstOrDefaultAsync();

    public async Task<Review> AddReview(Review newReview)
    {
        try
        {
            await _context.ReviewsCollection.InsertOneAsync(newReview);
            return newReview;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Review> UpdateReview(Review newReview)
    {
        try
        {
            var filter = Builders<Review>.Filter.Eq("ReviewId", newReview.ReviewId);
            var update = Builders<Review>.Update.Set("Text", newReview.Text);
            var result = await _context.ReviewsCollection.UpdateOneAsync(filter, update);
            return await GetReview(newReview.ReviewId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<String> DeleteReview(string id)
    {
        try
        {
            var filter = Builders<Review>.Filter.Eq("ReviewId", id);
            var result = await _context.ReviewsCollection.DeleteOneAsync(filter);
            return "Deleted";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
