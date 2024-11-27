using Nms.Db.Entities;
using Nms.Db.Repositories.Base;
using Nms.Db.Repositories.Interfaces;

namespace Nms.Db.Repositories
{
    public class ScheduledMealPlanRepository : BaseRepository<ScheduledMealPlan>, IScheduledMealPlanRepository
    {
        private readonly NmsContext _context;

        public ScheduledMealPlanRepository(NmsContext context) : base(context)
        {
            _context = context;
        }
    }
}
