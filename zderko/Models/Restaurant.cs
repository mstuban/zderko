using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace zderko.Models
{
    public class Restaurant
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Ime restorana")]
        [Required]
        public string Name { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        [DisplayName("Ponuda jela")]
        public virtual ICollection<Dish> Dishes { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [DisplayName("Broj telefona")]
        [Required]
        public long PhoneNumber { get; set; }

        [DisplayName("Email adresa")]
        public string EmailAddress { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [DisplayName("Radno Vrijeme Od")]
        [Range(0, 24)]
        public int HourOfDayFrom { get; set; }

        [DisplayName("Radno Vrijeme Do")]
        [Range(0, 24)]
        public int HourOfDayTo { get; set; }

        public float GetAverageRating()
        {
            int sumOfRatings = 0;
            var context = new ZderkoDbContext();
            IEnumerable<Rating> restaurantRatings = context.Ratings.ToList().Where(r => r.RestaurantId == this.ID); 

            foreach (Rating r in restaurantRatings)
            {
                sumOfRatings += r.NumberOfStars;
            }

            context.Dispose();
            if (restaurantRatings.Count() > 0)
            {
                return sumOfRatings / restaurantRatings.Count();
            }
            else
            {
                return 0;
            }
        }
    }
}