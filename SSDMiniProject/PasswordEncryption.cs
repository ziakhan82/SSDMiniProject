using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSDMiniProject
{
    public static class PasswordEncryption
    {
        public static string EncryptPassword(string password, byte[] salt, byte[] iv, string masterPassword)
        {
            using (Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(masterPassword, salt, 10000))
            {
                byte[] key = rfc2898.GetBytes(32); // 256-bit key

                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(password);
                            }
                        }

                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            
        }
        }

        public static string DecryptPassword(string encryptedPassword, byte[] salt, byte[] iv, string masterPassword)
        {
            using (Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(masterPassword, salt, 10000))
            {
                byte[] key = rfc2898.GetBytes(32); // 256-bit key

                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
       






    }
}