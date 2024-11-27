using Nms.Db.Entities;
using Nms.Db.Repositories;
using Nms.Db.Repositories.Interfaces;
using Nms.Services.Services.Interfaces;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Nms.Services.Services
{
    public class FoodService : IFoodService
    {
        public IFoodRepository _foodRepository { get; }

        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<List<Food>> GetAllByUserIdAsync(int userId)
        {
            return await _foodRepository.FindAllByWhereAsync(x => x.UserId == userId);
        }

        public async Task<Food> GetByIdAsync(int id)
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
    }
}
