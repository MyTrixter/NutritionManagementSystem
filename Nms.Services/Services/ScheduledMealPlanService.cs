using Nms.Db.Repositories.Interfaces;
using Nms.Services.Services.Interfaces;

namespace Nms.Services.Services
{
    public class ScheduledMealPlanService : IScheduledMealPlanService
    {
        public ScheduledMealPlanService(IScheduledMealPlanRepository scheduledMealPlanRepository)
        {
            _scheduledMealPlanRepository = scheduledMealPlanRepository;
        }

        public IScheduledMealPlanRepository _scheduledMealPlanRepository { get; }
    }
}
