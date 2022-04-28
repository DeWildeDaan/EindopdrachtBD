namespace Project.Repositories;

public interface IUserRepository
{
    Task<User> AddUser(User user);
    Task<User> GetUser(string username, string password);
}

public class UserRepository : IUserRepository
{
    private readonly IMongoContext _context;
    public UserRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<User> GetUser(string username, string password) => await _context.UsersCollection.Find<User>(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();

    public async Task<User> AddUser(User user)
    {
        try
        {
            await _context.UsersCollection.InsertOneAsync(user);
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}