using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSDMiniProject
{
    public static class StrongPassword
    {
        public static bool IsStrongPassword(string password)
        {
            // Define regular expressions for each criterion
            string lengthPattern = @".{8,}";
            string uppercasePattern = @"[A-Z]";
            string lowercasePattern = @"[a-z]";
            string digitPattern = @"\d";
            string specialCharacterPattern = @"[@!#\$%^&*()]"; // Adjust as needed

            // Check each criterion
            bool hasLength = Regex.IsMatch(password, lengthPattern);
            bool hasUppercase = Regex.IsMatch(password, uppercasePattern);
            bool hasLowercase = Regex.IsMatch(password, lowercasePattern);
            bool hasDigit = Regex.IsMatch(password, digitPattern);
            bool hasSpecialCharacter = Regex.IsMatch(password, specialCharacterPattern);

            // All criteria must be met
            return hasLength && hasUppercase && hasLowercase && hasDigit && hasSpecialCharacter;
        }

        public static bool VerifyMasterPassword(string enteredPassword, string storedPassword, byte[] storedSalt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
                byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSalt.Length];
                enteredPasswordBytes.CopyTo(saltedPassword, 0);
                storedSalt.CopyTo(saltedPassword, enteredPasswordBytes.Length);

                byte[] hashedPassword = sha256.ComputeHash(saltedPassword);

                // Convert the stored password from Base64 string to a byte array
                byte[] storedPasswordBytes = Convert.FromBase64String(storedPassword);

                // Compare the computed hash with the stored hash
                return storedPasswordBytes.SequenceEqual(hashedPassword);
            }
        }
    }
}



