using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace zderko.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public int NumberOfStars { get; set; }

        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}