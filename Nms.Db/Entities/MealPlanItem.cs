using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class MealPlanItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        [Required]
        public Guid FoodId { get; set; }
        public Food Food { get; set; }

        public decimal Quantity { get; set; }
    }

}
