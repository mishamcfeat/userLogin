using System;
using loginVerification.Interfaces;
using loginVerification.Services;
using loginVerification.Models;

// Define the namespace for the program
namespace loginVerification
{
    // Define the main program class
    class Program
    {
        // Define the main method
        static void Main()
        {
            // Create instances of the database and user services
            IDatabaseService databaseService = new DatabaseService();
            IUserService userService = new UserService(databaseService);

            // Define the options for the user
            string options = "1. Login\n2. Register\n3. Exit";
            int choice = 0;

            // Start a loop for the user to choose an option
            do
            {
                // Display the options to the user
                Console.WriteLine("Choose one of the following options: ");
                Console.WriteLine(options);

                // Get the user's choice
                string input = Console.ReadLine();

                // Validate the user's choice
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                // If the user chose to login
                if (choice == 1)
                {
                    // Allow the user to attempt to login up to 3 times
                    int loginAttempts = 0;
                    while (loginAttempts < 3)
                    {
                        // Get the user's username and password
                        Console.WriteLine("Enter username: ");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        string password = Console.ReadLine();

                        // If the login is successful, break the loop
                        if (userService.Login(username, password))
                        {
                            Console.WriteLine("Login successful!");
                            break;
                        }
                        else
                        {
                            // If the login is not successful, increment the login attempts and try again
                            Console.WriteLine("Invalid username or password. Please try again.");
                            loginAttempts++;
                            if (loginAttempts == 3)
                            {
                                Console.WriteLine("You have reached the maximum number of login attempts. Goodbye!");
                                return;
                            }
                        }
                    }
                }

                // If the user chose to register
                else if (choice == 2)
                {
                    // Get the user's desired username and password
                    Console.WriteLine("Enter username: ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    string password = Console.ReadLine();

                    // Create a new user with the provided username and password
                    User user = new User { Username = username, Password = password };

                    // Attempt to register the new user
                    if (userService.Register(user))
                    {
                        Console.WriteLine("Registration successful!");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Oops! Something went wrong. Please try again.");
                    }

                }
                // If the user chose to exit
                else if (choice == 3)
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
            } while (choice <= 0 || choice >= 4); // Continue the loop until the user chooses a valid option
        }
    }
}