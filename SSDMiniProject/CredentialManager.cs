using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSDMiniProject
{
    public static class CredentialManager
    {
        public static void StorePassword(AppDbContext context, UserAccount userAccount)
        {
            Console.Write("Enter the service name: ");
            string serviceName = Console.ReadLine();

            // Use the PasswordGenerator to create a strong password
            string password = PasswordGenerator.GeneratePassword(
                length: 16,            // Adjust the length as needed
                useUppercase: true,
                useLowercase: true,
                useDigits: true,
                useSpecialChars: true
            );

            Console.WriteLine($"Generated password: {password}");

            Console.Write("Enter the username: ");
            string username = Console.ReadLine();

            Console.Write("Enter additional information (optional): ");
            string additionalInfo = Console.ReadLine();

            // Generate salt and IV
            byte[] salt;
            byte[] iv;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                salt = new byte[32];
                rng.GetBytes(salt);

                iv = new byte[16]; // 128 bits for AES encryption
                rng.GetBytes(iv);
            }

            // Encrypt the password
            string encryptedPassword = PasswordEncryption.EncryptPassword(password, salt, iv, userAccount.HashedPassword);

            // Create a new Credential entity and save it to the database
            var newCredential = new Credential
            {
                ServiceName = serviceName,
                Username = username,
                Salt = salt,
                Iv = iv,
                EncryptedPassword = encryptedPassword,
                AdditionalInfo = additionalInfo
            };

            context.Credentials.Add(newCredential);
            context.SaveChanges();

            Console.WriteLine("Password has been successfully stored.");
        }







        public static string RetrievePassword(AppDbContext context, string serviceName, UserAccount userAccount)
        {
            var credential = context.Credentials.FirstOrDefault(c => c.ServiceName == serviceName);

            if (credential == null)
            {
                return "Service not found"; // Or you can handle this case as needed
            }

            // Decrypt the password
            string decryptedPassword = PasswordEncryption.DecryptPassword(credential.EncryptedPassword, credential.Salt, credential.Iv, userAccount.HashedPassword);

            return decryptedPassword;
        }
    }
}