using loginVerification.Interfaces;
using loginVerification.Models;

namespace loginVerification.Services;

public class UserService : IUserService
{
    private readonly IDatabaseService _databaseService;

    public UserService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Login(string username, string password)
    {
        return _databaseService.CheckLogin(username, password);
    }

    public bool Register(User user)
    {
        // Implement register logic here
        return true;
    }
}