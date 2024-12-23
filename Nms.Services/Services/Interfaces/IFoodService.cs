﻿using Nms.Core.Enums;
using Nms.Db.Entities;
using System.Linq.Expressions;

namespace Nms.Services.Services.Interfaces
{
    public interface IFoodService
    {
        Task<List<Food>> GetAllByUserIdAsync(int userId);
        Task<Food> GetByIdAsync(int id);
        Task<Food> UpdateAsync(Food food);
        Task<Food> CreateAsync(Food food);
        Task DeleteAsync(int id);
        Task<List<Food>> GetFilteredAndSortedFoods(int userId, string filterName, SortOrder? sortOrder);
        public Task<List<Food>> GetAllByWhereOrderedAscendingAsync(
            Expression<Func<Food, bool>> match,
            Expression<Func<Food, object>> orderBy);

        public Task<List<Food>> GetAllByWhereOrderedDescendingAsync(
            Expression<Func<Food, bool>> match,
            Expression<Func<Food, object>> orderBy
          );
    }
}
