using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace zderko.Models
{
    public class Dish
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Opis")]
        public string Description { get; set; }

        [ForeignKey("Restaurant")]
        [DisplayName("Restoran")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Ime mora biti između 5 i 50 znakova.")]
        [Required]
        [DisplayName("Naziv")]
        public string Name { get; set; }

        [Range(0, 500, ErrorMessage = "Cijena mora biti veća od nule i manja od 500.")]
        [Required]
        [DisplayName("Cijena")]
        public float Price { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public float GetAverageRating()
        {
            int sumOfRatings = 0;
            var context = new ZderkoDbContext();
            IEnumerable<Rating> ratings = context.Ratings.ToList().Where(r => r.DishId == this.ID);

            foreach (Rating r in ratings)
            {
                sumOfRatings += r.NumberOfStars;
            }

            context.Dispose();
            if (ratings.Count() > 0)
            {
                return sumOfRatings / ratings.Count();
            }
            else
            {
                return 0;
            }
        }
    }
}