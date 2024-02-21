using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SlackApi.Utils
{
    public class DataEncryptor
    {
        private const string Key = "LJQKGZ4rYLDCLJQKGZ4rYLDC"; // This should be a securely generated key
        private const string IV = "12345678"; // Initialization Vector (IV) for TripleDES

        public string EncryptData(string data)
        {
            byte[] encryptedBytes;
            using (TripleDES desAlg = TripleDES.Create())
            {
                desAlg.Key = Encoding.UTF8.GetBytes(Key);
                desAlg.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform encryptor = desAlg.CreateEncryptor(desAlg.Key, desAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(data);
                        }
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public string DecryptData(string encryptedData)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedData);
            string plaintext = null;

            using (TripleDES desAlg = TripleDES.Create())
            {
                desAlg.Key = Encoding.UTF8.GetBytes(Key);
                desAlg.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform decryptor = desAlg.CreateDecryptor(desAlg.Key, desAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
