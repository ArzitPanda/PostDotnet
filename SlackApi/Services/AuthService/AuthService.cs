using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SlackApi.Utils;

namespace SlackApi.Services.AuthService
{
    public class AuthService : IAuthService
    {


        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }



        public async Task<User> SignUp(UserAuthDto auth)
        {

            //first step create the user
            User user = new User
            {
                DateOfBirth = auth.DateOfBirth,
                PhotoUrl = auth.PhotoUrl,
                UserEmail = auth.UserEmail,
                UserName = auth.UserName,
                UserPhone = auth.UserPhone,
            };

             var user1 =  await  _unitOfWork.UserRepository.Insert(user);
            PasswordManagerUtils.PasswordCreator(auth.Password, out byte[] passwordHash, out byte[] salt);
            
            var PasswordManager = new UserPasswordManager { PasswordHash = passwordHash, PasswordSalt = salt, User = user1 };

            var data = _unitOfWork.CredentialRepository.Insert(PasswordManager);

            return user1;

            
            




           
        }
    }
}
