using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class MealPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<MealPlanItem> MealPlanItems { get; set; } = new List<MealPlanItem>();
    }

}
