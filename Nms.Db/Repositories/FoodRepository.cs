using Nms.Db.Entities;
using Nms.Db.Repositories.Base;
using Nms.Db.Repositories.Interfaces;

namespace Nms.Db.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        private readonly NmsContext _context;

        public FoodRepository(NmsContext context) : base(context)
        {
            _context = context;
        }
    }
}
