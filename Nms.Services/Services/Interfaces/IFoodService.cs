using Nms.Db.Entities;

namespace Nms.Services.Services.Interfaces
{
    public interface IFoodService
    {
        Task<List<Food>> GetAllByUserIdAsync(int userId);
        Task<Food> GetByIdAsync(int id);
        Task<Food> UpdateAsync(Food food);
        Task<Food> CreateAsync(Food food);
    }
}
