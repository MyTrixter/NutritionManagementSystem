using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class ScheduledMealPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public DateOnly Date { get; set; }
    }

}
