using Nms.Db.Entities;
using Nms.Db.Repositories.Base;
using Nms.Db.Repositories.Interfaces;

namespace Nms.Db.Repositories
{
    public class MealPlanRepository : BaseRepository<MealPlan>, IMealPlanRepository
    {
        private readonly NmsContext _context;

        public MealPlanRepository(NmsContext context) : base(context)
        {
            _context = context;
        }
    }
}
