using System;
using loginVerification.Interfaces;
using loginVerification.Services;
using loginVerification.Models;

namespace loginVerification
{
    class Program
    {
        static void Main()
        {
            IDatabaseService databaseService = new DatabaseService();
            IUserService userService = new UserService(databaseService);

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
                    int loginAttempts = 0;
                    while (loginAttempts < 3)
                    {
                        Console.WriteLine("Enter username: ");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        string password = Console.ReadLine();

                        if (userService.Login(username, password))
                        {
                            Console.WriteLine("Login successful!");
                            break;
                        }
                        else
                        {
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
                
                else if (choice == 2)
                {
                    Console.WriteLine("Enter username: ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    string password = Console.ReadLine();

                    User user = new User { Username = username, Password = password };

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
                else if (choice == 3)
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
            } while (choice <= 0 || choice >= 4);
        }
    }
}