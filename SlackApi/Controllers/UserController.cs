using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Exceptions;
using SlackApi.Services.UserService;
using System.Threading.Tasks;

namespace SlackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
          
                var user = await _userService.GetUserById(id);
               

                return Ok(user);
          
          
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            var user = await _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }



        [EnableQuery]
        [HttpGet]
        public async Task<IQueryable<User>> GetAll()
        {
           var user = await _userService.GetAllUsers();

            return user;
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UpdateUserDto updateUserDto)
        {
            updateUserDto.Id = id;
            if (!await _userService.UpdateUser(updateUserDto))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (!await _userService.DeleteUser(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
