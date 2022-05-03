namespace Project.Service;

public class QuestionService
{
    private readonly string _wordsapi;

    public QuestionService(IOptions<ApiSettings> apiSettings){
        _wordsapi = 
    }

    public async Task<Boolean> CheckBadWords()
    {
        
        return await GetRestaurants();
    }
}