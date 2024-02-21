// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

namespace console.Mains
{
 public class Program
    {



        public static void Main(string[] args)
        {
            byte[] randomBytes = new byte[24];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
           Console.WriteLine(Convert.ToBase64String(randomBytes));


        }
    }


}