using Nms.Db.Entities;

namespace Nms.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);  
    }
}
