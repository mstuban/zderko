using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using zderko.Models;

namespace zderko.Controllers
{
    [RoutePrefix("Restaurant")]
    public class RestaurantController : Controller
    {
        public object TEntityCollection { get; private set; }

            public ActionResult Index()
            {
                var context = new ZderkoDbContext();
                List<Restaurant> restaurants = context.Restaurants.Include(r => r.Address).ToList();
                context.Dispose();
                return View(restaurants);
            }

            public ActionResult Details(int? id = null)
            {
                var context = new ZderkoDbContext();

                if (id == null)
                    return View();

                var model = context.Restaurants.Include(r => r.Address).Include(r => r.User).SingleOrDefault(x => x.ID == id);
                context.Dispose();
                return View(model);
            }

            [Authorize(Roles = "RESTAURANT_MANAGER")]
            public ActionResult Create()
            {
                //this.FillDropDownValues();

                return View();
            }

            [Authorize(Roles = "RESTAURANT_MANAGER")]
            [HttpPost]
            public ActionResult Create(Restaurant restaurant)
            {
                var context = new ZderkoDbContext();

                if (ModelState.IsValid)
                {
                    string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

                    restaurant.UserId = userId;
                    context.Restaurants.Add(restaurant);

                    context.SaveChanges();
                    context.Dispose();
                    return RedirectToAction("Index");
                }

                return View(restaurant);
            }

            [Authorize(Roles = "RESTAURANT_MANAGER")]
            public ActionResult Edit(int id)
            {
                var context = new ZderkoDbContext();
            //this.FillDropDownValues();

            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var restaurant = context.Restaurants.Include(r => r.Address).SingleOrDefault(x => x.ID == id);

            if(restaurant.UserId != userId)
            {
                string model = null;
                return View(model);       
            }

            context.Dispose();
                return View(restaurant);
            }

            [Authorize(Roles = "RESTAURANT_MANAGER")]
            [HttpPost]
            [ActionName("Edit")]
            public ActionResult EditPost(int id)
            {
                var context = new ZderkoDbContext();
                var result = context.Restaurants.Find(id);

                var didUpdateModelSucceed = this.TryUpdateModel(result);

                if (didUpdateModelSucceed && ModelState.IsValid)
                {
                    context.SaveChanges();
                    context.Dispose();
                    return RedirectToAction("Index");
                }

                //this.FillDropDownValues();

                return View(result);
            }

            [Authorize(Roles = "RESTAURANT_MANAGER")]
            [HttpPost]
            public ActionResult Delete(int id)
            {
                var context = new ZderkoDbContext();
                Restaurant result = context.Restaurants.Find(id);

            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                
            if(result.UserId != userId)
            {
                return Json(new { result = "failure" });
            }

            context.Entry(result.Address).State = System.Data.Entity.EntityState.Deleted;

            foreach (var order in result.Orders.ToList())
            {
                context.Orders.Remove(order);
            }

            foreach (var dish in result.Dishes.ToList())
            {
                context.Dishes.Remove(dish);
            }

            context.Entry(result).State = System.Data.Entity.EntityState.Deleted;
    
                context.SaveChanges();

                context.Dispose();

                return Json(new { result = "success" });
            }

        [Route("{restaurantId}/Dishes")]
        public ActionResult Dishes(int? restaurantId = null)
            {

            if (restaurantId == null)
            {
                return View();
            }

            var context = new ZderkoDbContext();

            List<Dish> Dishes = context.Dishes.Include(r => r.Restaurant).Include(r => r.Ratings).Where(d => d.RestaurantId == restaurantId).ToList();
                context.Dispose();
                return View(Dishes);
            }

        [Authorize]
        [HttpGet]
        public ActionResult IndexAjax()
        {
            var context = new ZderkoDbContext();

            IEnumerable<Restaurant> dataQuery = context.Restaurants.Include(c => c.Address).Include(r => r.Dishes).ToList();

            //context.Dispose();
            return PartialView("_IndexTable", dataQuery.ToList());
        }

        [HttpPost]
        public ActionResult GetByAddressStreet(string streetAddress)
        {
            var context = new ZderkoDbContext();

            if (streetAddress == null || streetAddress.Length == 0) {
                var allRestaurants = context.Restaurants.ToList();
                return View("Index", allRestaurants);
            }

            List<Restaurant> filteredRestaurants = new List<Restaurant>();
            var restaurantList = context.Restaurants.Include(r => r.Address).Include(r => r.User).ToList();

            foreach(var r in restaurantList)
            {   
                if(streetAddress.Contains(r.Address.City) || r.Address.City.Contains(streetAddress) || streetAddress.Contains(r.Address.Street) || r.Address.Street.Contains(streetAddress) || streetAddress.Contains(r.Address.HouseNumber) || r.Address.HouseNumber.Contains(streetAddress))
                {
                    filteredRestaurants.Add(r);
                }
            }

            context.Dispose();
            return View("Index", filteredRestaurants);
        }
    }
}