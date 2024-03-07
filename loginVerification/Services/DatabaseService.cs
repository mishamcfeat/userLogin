// Import necessary namespaces
using System.Data;
using Npgsql;
using BCrypt.Net;
using loginVerification.Interfaces;
using loginVerification.Models;

// Define the namespace for the service classes
namespace loginVerification.Services;

// DatabaseService class implements the IDatabaseService interface
public class DatabaseService : IDatabaseService
{
    // Method to check if a user's login credentials are valid
    public bool CheckLogin(string username, string password)
    {
        // Use the connection string from the DatabaseConfig class to establish a connection
        using (var conn = new NpgsqlConnection(DatabaseConfig.ConnString))
        {
            conn.Open();
            
            // SQL query to get the user's details from the database
            string query = "SELECT username, password FROM users WHERE username = @username";
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                // Add the username parameter to the SQL query
                cmd.Parameters.AddWithValue("username", username);

                // Execute the SQL query and get the result
                using (var reader = cmd.ExecuteReader())
                {
                    // If a user is found with the given username
                    if (reader.Read())
                    {
                        // Get the stored password from the database
                        string storedPassword = reader.GetString(1);
                        // Check if the given password matches the stored password
                        bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(password, storedPassword);
                        // Return the result of the password check
                        return isPasswordMatch;
                    }
                }
            }
        }
        // If no user is found with the given username, return false
        return false;
    }

    // Method to register a new user
    public bool RegisterUser(User user)
    {
        // Use the connection string from the DatabaseConfig class to establish a connection
        using (var conn = new NpgsqlConnection(DatabaseConfig.ConnString))
        {
            conn.Open();

            // Hash the user's password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // SQL query to insert the new user's details into the database
            string query = "INSERT INTO users (username, password) VALUES (@username, @password)";
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                // Add the username and hashed password parameters to the SQL query
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.Parameters.AddWithValue("password", hashedPassword);

                // Execute the SQL query and get the number of affected rows
                int result = cmd.ExecuteNonQuery();

                // If a row was inserted, return true
                return result == 1;
            }
        }
    }
}