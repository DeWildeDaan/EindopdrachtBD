public record UserInfo(string username,string name,string city);
public record AuthenticationRequestBody(string UserName,string Password);

public interface IAuthenticationService
{
    Task<String> Authenticate(AuthenticationRequestBody body);
    Task Setup();
    Task<UserInfo> ValidateUser(string username, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationSettings _authsettings;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IOptions<AuthenticationSettings> settings, IUserRepository userRepository)
    {
        _authsettings = settings.Value;
        _userRepository = userRepository;
    }

    public async Task Setup()
    {
        User user = new User() { UserName = "Daan De Wilde", Password = "daandewilde", NickName = "Daan", Location = "Zele" };
        await _userRepository.AddUser(user);
    }

    public async Task<UserInfo> ValidateUser(string username, string password)
    {
        User user = await _userRepository.GetUser(username, password);
        return new UserInfo(user.UserName, user.UserName, user.Location);
    }

    public async Task<String> Authenticate(AuthenticationRequestBody body)
    {
        UserInfo user = await ValidateUser(body.UserName, body.Password);
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_authsettings.SecretForKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("sub", "1"));
        claimsForToken.Add(new Claim("given_name", user.name));
        claimsForToken.Add(new Claim("city", user.city));

        var jwtSecurityToken = new JwtSecurityToken(
            _authsettings.Issuer,
            _authsettings.Audience,
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
        );
        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return tokenToReturn;
    }
}