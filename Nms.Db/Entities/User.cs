using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
   
        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; }

        public ICollection<Food> Foods { get; set; } 
        public ICollection<MealPlan> MealPlans { get; set; } 
    }
}
