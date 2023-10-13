using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSDMiniProject
{
    public static class PasswordGenerator
    {
        public static string GeneratePassword(int length, bool useUppercase, bool useLowercase, bool useDigits, bool useSpecialChars)
        {
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:'\",.<>?";

            StringBuilder validChars = new StringBuilder();
            if (useUppercase)
                validChars.Append(uppercaseChars);
            if (useLowercase)
                validChars.Append(lowercaseChars);
            if (useDigits)
                validChars.Append(digitChars);
            if (useSpecialChars)
                validChars.Append(specialChars);

            if (validChars.Length == 0)
            {
                throw new ArgumentException("At least one character type (uppercase, lowercase, digits, special characters) must be selected.");
            }

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                StringBuilder password = new StringBuilder(length);
                foreach (byte randomByte in randomBytes)
                {
                    password.Append(validChars[randomByte % validChars.Length]);
                }

                return password.ToString();
            }
        }
    }
}

