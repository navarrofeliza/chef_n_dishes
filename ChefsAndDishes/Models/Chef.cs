using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsAndDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public DateTime Birthday { get; set; }
        public int Age
        {
            get { return DateTime.Now.Year - Birthday.Year; }
        }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Dish> Dishes { get; set; }
    }
}