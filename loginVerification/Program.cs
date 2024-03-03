using System;

using System;

namespace loginVerification
{
    class Program
    {
        static void Main()
        {
            // Create an instance of the LoginSystem class
            LoginSystem loginSystem = new LoginSystem();

            // Call the Login method
            bool loginSuccessful = loginSystem.Login();

            // If the login was not successful, terminate the program
            if (!loginSuccessful)
            {
                return;
            }

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
        private const int maxLoginAttempts = 3;

        // Hardcoded username and password for the login system
        // In a real-world application, these would be securely stored and retrieved
        private string storedUsername = "admin";
        private string storedPassword = "Pass123!";

        public bool Login()
        {
            // Initialise the number of login attempts to 0
            int loginAttempts = 0;

            // Start a loop that continues until a break statement is encountered
            do
            {
                // If the number of login attempts is greater than or equal to the maximum allowed,
                // inform the user and return false
                if (loginAttempts >= maxLoginAttempts)
                {
                    Console.WriteLine("You have reached the maximum number of login attempts. Please try again later.");
                    return false;
                }

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
                // inform the user of a successful login and return true
                if (username.Equals(storedUsername, StringComparison.OrdinalIgnoreCase) && password == storedPassword)
                {
                    Console.WriteLine("Login successful!");
                    return true;
                }
                // If the entered username and password do not match the stored ones,
                // inform the user, increment the login attempts counter, and continue to the next loop iteration
                else
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                    loginAttempts++;
                }
                // End of the do-while loop
            } while (true);
        }
    }
}
