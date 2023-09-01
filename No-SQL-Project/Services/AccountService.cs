using Microsoft.Extensions.Options;
using MongoDB.Driver;
using No_SQL_Project.Controllers.Dtos;
using No_SQL_Project.Models;
using No_SQL_Project.Utils;

namespace No_SQL_Project.Services;

public class AccountService
{
    private readonly IConfiguration _config;
    private readonly TokenProvider _tokenProvider;
    private readonly IMongoCollection<User> _usersCollection;

    public AccountService(IConfiguration config, IOptions<MoviesDatabaseSettings> movieDbSettigs)
    {
        _config = config;
        _tokenProvider = new(_config);

        var mongoClient = new MongoClient(
            movieDbSettigs.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            movieDbSettigs.Value.DatabaseName);
        
        _usersCollection = mongoDatabase.GetCollection<User>(movieDbSettigs.Value.UsersCollectionName);
    }
    
    public string Login(UserLoginRequest userLoginInfo)
    {
        User user = Authenticate(userLoginInfo.Email, userLoginInfo.Password);
        
        string jwtToken = _tokenProvider.GenerateJwtToken(user);

        return jwtToken;
    }
    
    private User Authenticate(string email, string password)
    {
        User user = _usersCollection
            .Find(x => x.UserEmail == email && x.Password == password)
            .FirstOrDefault();
        
        if (user is null)
        {
            throw new AppException(ErrorMessages.INVALID_EMAIL_OR_PASSWORD);
        }
        
        if (email != user.UserEmail)
        {
            throw new AppException(ErrorMessages.INVALID_EMAIL);
        }
        
        if (password != user.Password)
        {
            throw new AppException(ErrorMessages.INVALID_PASSWORD);
        }

        return user;
    }
}