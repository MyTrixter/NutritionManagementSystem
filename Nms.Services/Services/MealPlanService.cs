using Nms.Db.Repositories.Interfaces;
using Nms.Services.Services.Interfaces;

namespace Nms.Services.Services
{
    public class MealPlanService : IMealPlanService
    {
        public MealPlanService(IMealPlanRepository mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }

        public IMealPlanRepository _mealPlanRepository { get; }
    }
}
