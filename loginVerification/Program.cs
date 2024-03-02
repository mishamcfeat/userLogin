using System;

namespace loginVerification
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Enter username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Please Enter Username and Password!");
                    continue;
                }

                if (username.ToLower() == "admin" && password == "PassWord123!")
                {
                    Console.WriteLine("Login successful!");
                    break;
                }
            } while (true);



        }
    }
}
