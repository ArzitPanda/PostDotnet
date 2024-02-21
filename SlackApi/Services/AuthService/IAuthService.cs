using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SlackApi.Services.AuthService
{
    public interface IAuthService
    {
        public Task<User> SignUp(UserAuthDto auth);



    }
}
