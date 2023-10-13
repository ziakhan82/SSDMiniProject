using SSDMiniProject;
using System;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("****  Welcome to My Password Manager *******");
Console.WriteLine();
bool loggedIn = false; // Track whether the user is logged in

UserAccount userAccount = null;
AppDbContext context = new AppDbContext(); // Declare context outside the using block

userAccount = context.UserAccounts.FirstOrDefault();
if (userAccount == null)
{
    string masterPassword;
    string confirmPassword;
    bool passwordIsValid = false;

    Console.WriteLine("Create a strong master password. It should be at least 8 characters long, " +
        "contain both uppercase and lowercase letters, at least one digit, and a special character " +
        "like '@', '!', '#', '$', '%', '^', '&', or '*'.");
    Console.WriteLine("Example: My$ecureP@ssw0rd");

    do
    {
        Console.WriteLine("Create a strong master password:");
        masterPassword = Console.ReadLine();

        Console.WriteLine("Confirm your master password:");
        confirmPassword = Console.ReadLine();

        if (masterPassword != confirmPassword)
        {
            Console.WriteLine("Passwords do not match. Please try again.");
            continue;
        }

        if (!StrongPassword.IsStrongPassword(masterPassword))
        {
            Console.WriteLine("Weak password. Please choose a stronger password. Example: My$ecureP@ssw0rd");
        }
        else
        {
            passwordIsValid = true; // Mark the password as valid
        }
    } while (!passwordIsValid);

    // Password is strong, proceed with hashing and storing it
    byte[] salt;
    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
    {
        byte[] saltBytes = new byte[32]; // 32 bytes for a strong salt
        rng.GetBytes(saltBytes);
        salt = saltBytes;
    }

    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(masterPassword);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];
        passwordBytes.CopyTo(saltedPassword, 0);
        salt.CopyTo(saltedPassword, passwordBytes.Length);

        byte[] hashedPassword = sha256.ComputeHash(saltedPassword);

        var newUserAccount = new UserAccount
        {
            HashedPassword = Convert.ToBase64String(hashedPassword),
            Salt = salt,
        };

        context.UserAccounts.Add(newUserAccount);
        context.SaveChanges();

        Console.WriteLine("Strong password accepted and stored, please keep it safe!");
    }
}
else
{
    // User is not logged in
    loggedIn = false;
}

if (!loggedIn) // Check if the user is not logged in
{
    // Ask the user to log in immediately
    while (true)
    {
        Console.Write("Enter your master password to log in: ");
        string enteredPassword = Console.ReadLine();

        userAccount = context.UserAccounts.FirstOrDefault();

        if (StrongPassword.VerifyMasterPassword(enteredPassword, userAccount.HashedPassword, userAccount.Salt))
        {
            Console.WriteLine($"Logged in successfully!");

            Console.WriteLine($"Stored Password: {userAccount.HashedPassword}");
            // Password is correct, allow access to the stored credentials

            Console.WriteLine("Access granted. You can now manage your credentials.");
            Console.WriteLine("");
            loggedIn = true; // Mark the login as successful
            break;
        }
        else
        {
            Console.WriteLine("Incorrect master password. Access denied.");
        }
    }
}

if (loggedIn)
{
    // User is logged in, display the menu
    while (true)
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("q: Quit the program");
        Console.WriteLine("s: Store a password");
        Console.WriteLine("r: Retrieve a password"); // Add a new command
        Console.Write("Enter a command: ");

        string command = Console.ReadLine();

        switch (command.ToLower())
        {
            case "q":
                Console.WriteLine("Exiting the program.");
                return; // Exit the program

            case "s":
                Console.WriteLine("Storing a password...");
                CredentialManager.StorePassword(context, userAccount);
                break;
                
            case "r":
                Console.WriteLine("Retrieving a password...");
                Console.Write("Enter the service name: ");
                string serviceNameToRetrieve = Console.ReadLine();
                string retrievedPassword = CredentialManager.RetrievePassword(context, serviceNameToRetrieve, userAccount);
                if (retrievedPassword == "Service not found")
                {
                    Console.WriteLine("Service not found.");
                }
                else
                {
                    Console.WriteLine($"Password for {serviceNameToRetrieve}: {retrievedPassword}");
                }
                break;

            default:
                Console.WriteLine("Invalid command. Please enter 'q' to quit or 's' to store a password.");
                break;
        }
    }
}