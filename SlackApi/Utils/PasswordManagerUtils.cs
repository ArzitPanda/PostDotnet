using System.Security.Cryptography;

namespace SlackApi.Utils
{
    public class PasswordManagerUtils
    {

        public static void PasswordCreator(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {

            using (var key = new HMACSHA512())
            {
                    
               passwordHash=  key.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = key.Key;
            }



        }


        public static bool CheckPassword(string password, byte[] storedPasswordHash, byte[] storedPasswordSalt)
        {
            using (var hmac = new HMACSHA512(storedPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedPasswordHash);
            }
        }







    }
}
