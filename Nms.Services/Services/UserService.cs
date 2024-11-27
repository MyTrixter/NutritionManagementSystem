using Nms.Db.Entities;
using Nms.Db.Repositories.Interfaces;
using Nms.Services.Services.Interfaces;

namespace Nms.Services.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<User> CreateAsync(User user)
        {
            return await _userRepository.InsertAsync(user);
        }
    }
}
