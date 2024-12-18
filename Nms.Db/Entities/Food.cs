using System.ComponentModel.DataAnnotations;

namespace Nms.Db.Entities
{
    public class Food
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Calories { get; set; }

        public decimal Protein { get; set; }

        public decimal Fats { get; set; }

        public decimal Carbohydrates { get; set; }

        public decimal Quantity { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
