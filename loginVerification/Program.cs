using System;
using System.Data;
using Npgsql;
using BCrypt.Net;


namespace loginVerification
{
    class Program
    {
        static void Main()
        {
            // Create an instance of the LoginSystem class
            LoginSystem login = new LoginSystem();
            RegisterSystem register = new RegisterSystem();
            
            string options = "1. Login\n2. Register\n3. Exit";
            int choice = 0;

            do
            {
                Console.WriteLine("Choose one of the following options: ");
                Console.WriteLine(options);
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                if (choice == 1)
                {
                    if (login.Login())
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password. Please try again.");
                    }
                }
                else if (choice == 2)
                {
                    if (register.Register())
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Oops! Something went wrong. Please try again.");
                    }

                }
                else if (choice == 3)
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
            } while (choice <= 0 || choice >= 4);
            

            string menu = "1. Change your username\n2. Change your password\n3. Add new member\n4. Delete member\n5. Exit";
            int decision = 0;

            do
            {
                Console.WriteLine("Make your decision out of the following options: ");
                Console.WriteLine(menu);
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out decision))
                {
                    Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Change your username");
                        //ChangeUsername();
                        break;
                    case "2":
                        Console.WriteLine("Change your password");
                        //ChangePassword();
                        break;
                    case "3":
                        Console.WriteLine("Add new member");
                        //AddNewMember();
                        break;
                    case "4":
                        Console.WriteLine("Delete member");
                        //DeleteMember();
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;
                }
            } while (decision <= 0 || decision >= 6);
        }
    }

    class LoginSystem
    {
        // Set the maximum number of login attempts to 3
        private const int maxLoginAttempts = 4;

        public bool Login()
        {
            // Initialise the number of login attempts to 0
            int loginAttempts = 0;

            // Start a loop that continues until a break statement is encountered
            while (loginAttempts < maxLoginAttempts)
            {

                // Prompt the user to enter their username & password
                Console.WriteLine("Enter username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine();

                // If either the username or password field is empty,
                // inform the user and increment the login attempts counter, then continue to the next loop iteration
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Please Enter Username and Password!");
                    loginAttempts++;
                    continue;
                }

                // If the entered username and password match the stored ones,
                // inform the user of a successful login and return true, else iterate loginAttempts
                if (checkLogin(username, password))
                {
                    Console.WriteLine("Login successful!");
                    return true;
                }

                Console.WriteLine("Invalid username or password. Please try again.");
                if (loginAttempts == (maxLoginAttempts - 1))
                {
                    Console.WriteLine("You have reached the maximum number of login attempts. Goodbye!");
                }

                loginAttempts++;
            }

            return false;
        }

        static bool checkLogin(string username, string password)
        {
            
            // Connection to my localhost database localVerification
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=9596;Database=loginVerification";

            // Open a connection to the database using Npgsql
            using (var conn = NpgsqlConnection(connString))
            {
                //Open the connection
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
    }
    
    
}
