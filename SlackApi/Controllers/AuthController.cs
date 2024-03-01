using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Services.AuthService;
using SlackApi.Services.UserService;
using SlackApi.Utils;
using SocialTree.Utils.Validator;

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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(IAuthService authService, IUserService userService, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _userService = userService;
            _httpClient = httpClientFactory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserAuthDto authDto)
        {
            try
            {
                var validator = new UserValidator();
                ValidationResult result = validator.Validate(authDto);


                if (result.IsValid)
                {
                    var user = await _authService.SignUp(authDto);
                    return Ok(user);
                }
                else
                {
                    var errors = new List<string>();
                    foreach (var failure in result.Errors)
                    {
                        errors.Add($"{failure.PropertyName}: {failure.ErrorMessage}");
                    }

                    return BadRequest(errors);
                }




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
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Console.WriteLine(responseBody);
                    string token = responseObject?.token;
                    var id = responseObject?.clientId;
                    HttpContext.Session.SetString("JwtToken", token);
                    return Redirect($"http://localhost:3000/call?response_type=authorization&id={id}&token={token}");
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





        [HttpPost("/store-token")]
        public IActionResult StoreTokenInSession(string token)
        {

            HttpContext.Session.SetString("JwtToken", token);
            return Ok();

        }



        [HttpGet("/get-token")]
        public IActionResult GetTokenFromSession()
        {
            try
            {
                // Retrieve the JWT token from the session
                var jwtToken = HttpContext.Session.GetString("JwtToken");

                if (jwtToken != null)
                {
                    // Return the JWT token
                    return Ok(new { token = jwtToken });
                }
                else
                {
                    // No token found in session
                    return NotFound("Token not found in session");
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
