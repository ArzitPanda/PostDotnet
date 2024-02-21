using Microsoft.AspNetCore.Mvc;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Services.AuthService;
using SlackApi.Services.UserService;
using SlackApi.Utils;
using System.Net.Http;

namespace SlackApi.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;
        public AuthController(IAuthService authService, IUserService userService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _userService = userService;
            _httpClient = httpClientFactory.CreateClient();

        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserAuthDto authDto)
        {
            try
            {
                var user = await _authService.SignUp(authDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("login")]
        public IActionResult LoginToAuthServer()
        {
            return Redirect("http://localhost:5002/Auth/login");

        }


        [HttpGet("callBack")]
        public async Task<IActionResult> CallBack(string code)
        {

            try
            {
                // Decrypt the code
                DataEncryptor dataEncryptor = new DataEncryptor();
                string decryptedCode = dataEncryptor.DecryptData(code);


                User user = await _userService.GetUserByUserName(decryptedCode);




                // Prepare the request data
                var requestData = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("clientID", user.UserId.ToString()),
                new KeyValuePair<string, string>("clientUserName", user.UserName),
                new KeyValuePair<string, string>("authorizationCode",code)
            });

                // Make the HTTP POST request to the auth server
                var response = await _httpClient.PostAsync("http://localhost:5002/Token", requestData);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Handle the case where the request fails
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    


        





    }
}
