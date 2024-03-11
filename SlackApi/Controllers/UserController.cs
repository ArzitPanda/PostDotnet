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
      

        public UserController(IUserService userService)
        {
            _userService = userService;
        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
          
               try
            {
                var user = await _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
          
          
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
          try
            {
                var user = await _userService.CreateUser(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
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
