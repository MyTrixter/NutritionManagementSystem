using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Food> Foods { get; set; } 
        public ICollection<MealPlan> MealPlans { get; set; } 
    }
}
