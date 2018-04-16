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
    public class Address
    {
        [Key]
        [ForeignKey("Restaurant")]
        public int ID { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        [DisplayName("Ime")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Ime mora biti između 5 i 50 znakova.")]
        [Required]
        public string Name { get; set; }
        
        [DisplayName("Poštanski broj")]
        [Required]
        public int PostalCode { get; set; }

        [DisplayName("Ulica")]
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Ulica mora biti između 5 i 50 znakova.")]
        public string Street { get; set; }

        [DisplayName("Grad")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Grad mora biti između 5 i 50 znakova.")]
        [Required]
        public string City { get; set; }

        [DisplayName("Država")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Država mora biti između 5 i 50 znakova.")]
        [Required]
        public string Country { get; set; }

        [DisplayName("Kućni broj")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Kućni broj mora biti između 1 i 10 znakova.")]
        [Required]
        public string HouseNumber { get; set; }
    }
}