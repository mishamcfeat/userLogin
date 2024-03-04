namespace loginVerification.Interfaces;

public interface IUserService
{
    bool Login(string username, string password);
    bool Register(User user);
}