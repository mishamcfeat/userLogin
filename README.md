# Login Verification

This project is a simple login verification system developed in C# and SQL. It allows users to register and login to a system. The system is case-insensitive for usernames and allows up to three attempts for incorrect logins.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 6.0 or higher
- PostgreSQL

### Installing

1. Clone the repository to your local machine.
2. Open the solution in JetBrains Rider or your preferred IDE.
3. Update the connection string in `DatabaseService.cs` with your PostgreSQL server details.
4. Run the `CreateUsersTable.sql` script in your PostgreSQL server to create the necessary database and table.
5. Build and run the project.

## Usage

The application presents the user with three options: Login, Register, and Exit.

- Login: Allows the user to login to the system. The user is given three attempts to enter the correct username and password.
- Register: Allows the user to register a new account. The user needs to provide a username and password.
- Exit: Exits the application.

## Built With

- C#
- SQL

## Contributing

Please read `CONTRIBUTING.md` for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

- Misha McFeat

See also the list of [contributors](https://github.com/yourusername/loginVerification/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the `LICENSE.md` file for details

## Acknowledgments

- JetBrains Rider: The IDE used for this project.
