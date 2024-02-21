namespace SlackApi.Utils
{
    public class AuthorizationCodeUtils
    {

        public static  string GenerateAuthorizationCode()
        {
            // Generate a random string for the authorization code
            var random = new Random();
            var code = Guid.NewGuid().ToString("N") + random.Next(1000, 9999); // Adding a random number for uniqueness
            return code;
        }


    }
}
