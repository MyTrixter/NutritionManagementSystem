using Nms.Db.Entities;
using Nms.Db.Repositories.Base;
using Nms.Db.Repositories.Interfaces;

namespace Nms.Db.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly NmsContext _context;

        public UserRepository(NmsContext context) : base(context)
        {
            _context = context;
        }
    }
}
