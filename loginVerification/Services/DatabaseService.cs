using System.Data;
using Npgsql;
using BCrypt.Net;
using loginVerification.Interfaces;
using loginVerification.Models;

namespace loginVerification.Services;

public class DatabaseService : IDatabaseService
{
    public bool CheckLogin(string username, string password)
    {
        // Implement database logic for checking login here
        string connString = "Host=localhost;Port=5432;Username=postgres;Password=9596;Database=loginVerification";

        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
            
            string query = "SELECT username, password FROM users WHERE username = @username";
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("username", username);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPassword = reader.GetString(1);
                        bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(password, storedPassword);
                        return isPasswordMatch;
                    }
                }
            }
        }
        return false;
    }

    public bool RegisterUser(User user)
    {
        // This would contain the login for registering a user
        return true;
    }
}