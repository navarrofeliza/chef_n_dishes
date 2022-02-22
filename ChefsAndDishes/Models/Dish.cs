using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsAndDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Dish's name must be longer than 2 characters!")]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Calories cannot exceed 1000 nor can it be negative!")]
        public int Calories { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Tastiness must be within 0 - 10 range!")]
        public int Tastiness { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required]
        public int ChefId { get; set; }
        public Chef Creator { get; set; }

    }
}
