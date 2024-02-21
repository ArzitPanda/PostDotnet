using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SlackApi.Services.UserService
{
    public interface IUserService
    {
        Task<User> CreateUser(UserCreateDto userDto);
        Task<bool> UpdateUser(UpdateUserDto updateUserDto);
        Task<bool> DeleteUser(long id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(long id);

        Task<User> GetUserByUserName(string userName);  
    }
}
