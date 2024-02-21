using AuthServerSlack.Models;
using Microsoft.AspNetCore.Mvc;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SlackApi.Utils;
using System.Data;
using System.Data.SqlClient;

namespace AuthServerSlack.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;



        public AuthController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            return View(); // Render the login view
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            // SQL query with parameters
            var data =await _unitOfWork.UserRepository.Find(a => a.UserName == username);

            User user = data.FirstOrDefault();

            var passwordGet = await _unitOfWork.CredentialRepository.Find(A => A.User.UserId == user.UserId);

            UserPasswordManager passwordManager = passwordGet.FirstOrDefault();


            if (PasswordManagerUtils.CheckPassword(password, passwordManager.PasswordHash, passwordManager.PasswordSalt))
            {

                DataEncryptor keyGen = new DataEncryptor();
                string a  = keyGen.EncryptData(user.UserName);


                return Redirect($"http://localhost:5283/api/Auth/callBack?code={a}");





            }
            else
            {
                return Unauthorized();
            }



         
        }


        [HttpPost("token")]
        public  IActionResult GetToken([FromForm] string clientID, [FromForm] string clientUserName, [FromForm] string authorizationCode)
        {

            Console.WriteLine("here i am");
            string GenerateTokenIs = JwtTokenGenerator.GenerateToken(clientID, clientUserName);


            GenerateJwtResponse g = new GenerateJwtResponse { ClientId=clientID,token=GenerateTokenIs};


            return Ok(g);

        }






    }





}
