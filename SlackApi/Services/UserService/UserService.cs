using AutoMapper;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;

using SlackApi.Exceptions;

namespace SlackApi.Services.UserService
{
    public class UserService :IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(UserCreateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            Console.WriteLine(user.ToString());
           var data = await _userRepository.Insert(user);
            return data;
        }

        public async  Task<bool> UpdateUser(UpdateUserDto updateUserDto)
        {
            var data = await _userRepository.Find(u => u.UserId == updateUserDto.Id);

            var user = data.FirstOrDefault();
            if (user == null)
                return false;

            user = _mapper.Map(updateUserDto, user);
           await _userRepository.Update(user);
            return true;
        }

        public async Task<bool> DeleteUser(long id)
        {
            return await _userRepository.Delete(id);
        }
        public async Task<IQueryable<User>> GetAllUsers()
        {
            return  _userRepository.GetAll();
        }

        public async  Task<User> GetUserById(long id)
        {
            var userdata = await _userRepository.Find(u => u.UserId == id);

            return userdata.FirstOrDefault() ?? throw new UserNotFoundException();
        }

        public async  Task<User> GetUserByUserName(string userName)
        {
            var userdata = await _userRepository.Find(u => u.UserName ==userName);

            return userdata.FirstOrDefault() ?? throw new UserNotFoundException();
        }
    }
}
