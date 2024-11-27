using Nms.Db.Repositories.Interfaces;
using Nms.Services.Services.Interfaces;

namespace Nms.Services.Services
{
    public class MealPlanItemService : IMealPlanItemService
    {
        public MealPlanItemService(IMealPlanItemRepository mealPlanItemRepository)
        {
            _mealPlanItemRepository = mealPlanItemRepository;
        }

        public IMealPlanItemRepository _mealPlanItemRepository { get; }
    }
}
