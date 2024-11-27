using Nms.Db.Entities;
using Nms.Db.Repositories.Base;
using Nms.Db.Repositories.Interfaces;

namespace Nms.Db.Repositories
{
    public class MealPlanItemRepository : BaseRepository<MealPlanItem>, IMealPlanItemRepository
    {
        private readonly NmsContext _context;

        public MealPlanItemRepository(NmsContext context) : base(context)
        {
            _context = context;
        }
    }
}
