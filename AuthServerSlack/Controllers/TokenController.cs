using AuthServerSlack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlackApi.Utils;

namespace AuthServerSlack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {




        [HttpPost]
        public IActionResult GetToken([FromForm] string clientID, [FromForm] string clientUserName, [FromForm] string authorizationCode)
        {

            Console.WriteLine("here i am");
            string GenerateTokenIs = JwtTokenGenerator.GenerateToken(clientID, clientUserName);

           
            GenerateJwtResponse g = new GenerateJwtResponse { ClientId = clientID, token = GenerateTokenIs };


            return Ok(g);

        }





    }
}
