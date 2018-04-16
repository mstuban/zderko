using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace zderko.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public long OrderNumber { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public DateTime OrderTime { get; set; }

        public MethodOfPayment MethodOfPayment { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}