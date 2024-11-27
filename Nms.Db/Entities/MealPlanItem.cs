using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class MealPlanItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        [Required]
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public decimal Quantity { get; set; }
    }

}
