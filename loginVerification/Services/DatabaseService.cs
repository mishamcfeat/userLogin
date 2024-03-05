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
        string connString = "Host=localhost;Port=5432;Username=postgres;Password=9596;Database=loginVerification";

        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            string query = "INSERT INTO users (username, password) VALUES (@username, @password)";
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.Parameters.AddWithValue("password", hashedPassword);

                int result = cmd.ExecuteNonQuery();

                // If the query successfully inserted a row, result will be 1
                return result == 1;
            }
        }
    }
}