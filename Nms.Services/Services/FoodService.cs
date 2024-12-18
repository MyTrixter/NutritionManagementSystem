using Nms.Core.Enums;
using Nms.Db.Entities;
using Nms.Db.Repositories.Interfaces;
using Nms.Services.Services.Interfaces;
using System.Linq.Expressions;

namespace Nms.Services.Services
{
    public class FoodService : IFoodService
    {
        public IFoodRepository _foodRepository { get; }

        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<List<Food>> GetAllByUserIdAsync(Guid userId)
        {
            return await _foodRepository.FindAllByWhereAsync(x => x.UserId == userId);
        }

        public async Task<Food> GetByIdAsync(Guid id)
        {
            var food = await _foodRepository.GetFirstWhereAsync(x => x.Id == id);

            if (food == null)
                throw new Exception("Food doesn't exist");

            return food;
        }

        public async Task<Food> UpdateAsync(Food food)
        {
            return await _foodRepository.UpdateAsync(food);
        }

        public async Task<Food> CreateAsync(Food food)
        {
            return await _foodRepository.InsertAsync(food);
        }

        public async Task<List<Food>> GetAllByWhereOrderedAscendingAsync(
                Expression<Func<Food, bool>> match, Expression<Func<Food, 
                object>> orderBy)
        {
            return await _foodRepository.GetAllByWhereOrderedAscendingAsync(match, orderBy);
        }

        public async Task<List<Food>> GetAllByWhereOrderedDescendingAsync(
                Expression<Func<Food, bool>> match, Expression<Func<Food,
                object>> orderBy)
        {
            return await _foodRepository.GetAllByWhereOrderedDescendingAsync(match, orderBy);
        }

        public async Task DeleteAsync(Guid id)
        {
            var food = await _foodRepository.GetFirstWhereAsync(x => x.Id == id);

            if (food == null)
                throw new Exception("Food doesn't exist");

            await _foodRepository.Delete(food);
        }

        public async Task<List<Food>> GetFilteredAndSortedFoods(Guid userId, string filterName, SortOrder? sortOrder)
        {
            var foods = await _foodRepository.FindAllByWhereAsync(x => x.UserId == userId);

            if (!string.IsNullOrEmpty(filterName))
            {
                foods = foods.Where(f => f.Name.Contains(filterName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            foods = sortOrder switch
            {
                SortOrder.NameAsc => foods.OrderBy(f => f.Name).ToList(),
                SortOrder.NameDesc => foods.OrderByDescending(f => f.Name).ToList(),
                SortOrder.CaloriesAsc => foods.OrderBy(f => f.Calories).ToList(),
                SortOrder.CaloriesDesc => foods.OrderByDescending(f => f.Calories).ToList(),
                _ => foods,
            };

            return foods;
        }
    }
}
