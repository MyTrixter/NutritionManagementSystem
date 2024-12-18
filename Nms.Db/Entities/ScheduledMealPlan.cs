using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class ScheduledMealPlan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public DateOnly Date { get; set; }
    }

}
