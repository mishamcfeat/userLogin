namespace loginVerification.Interfaces;

public interface IDatabaseService
{
    bool CheckLogin(string username, string password);
    bool RegisterUser(User user);
}