using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace zderko.Models
{
    public class User : IdentityUser
    {
        public string NameAndSurname { get; set; }
        
        public string OIB { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add customuser claims here
            return userIdentity;
        }
    }
}