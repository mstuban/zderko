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
    public class DishController : Controller
    {
        public ActionResult Index()
        {
            var context = new ZderkoDbContext();
            List<Dish> Dishes = context.Dishes.Include(d => d.Restaurant).Include(d => d.Ratings).ToList();
            context.Dispose();
            return View(Dishes);
        }

        public ActionResult Details(int? id = null)
        {
            var context = new ZderkoDbContext();

            if (id == null)
                return View();

            var model = context.Dishes.Include(r => r.Restaurant).Include(r => r.Ratings).SingleOrDefault(x => x.ID == id);
            context.Dispose();
            return View(model);
        }

        [Authorize(Roles = "RESTAURANT_MANAGER")]
        public ActionResult Create()
        {
            this.FillDropDownValues();

            return View();
        }

        [Authorize(Roles = "RESTAURANT_MANAGER")]
        [HttpPost]
        public ActionResult Create(Dish dish)
        {
            var context = new ZderkoDbContext();

            if (ModelState.IsValid)
            {
                context.Dishes.Add(dish);

                context.SaveChanges();
                context.Dispose();
                return RedirectToAction("Index");
            }

            return View(dish);
        }

        [Authorize(Roles = "RESTAURANT_MANAGER")]
        public ActionResult Edit(int id)
        {
            var context = new ZderkoDbContext();
            this.FillDropDownValues();

            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var dish = context.Dishes.Include(r => r.Restaurant).SingleOrDefault(x => x.ID == id);

            if (dish.Restaurant.UserId != userId)
            {
                string model = null;
                return View(model);
            }

            context.Dispose();
            return View(dish);
        }

        [Authorize(Roles = "RESTAURANT_MANAGER")]
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            var context = new ZderkoDbContext();
            var result = context.Dishes.Find(id);

            var didUpdateModelSucceed = this.TryUpdateModel(result);

            if (didUpdateModelSucceed && ModelState.IsValid)
            {
                context.SaveChanges();
                context.Dispose();
                return RedirectToAction("Index");
            }

            this.FillDropDownValues();

            return View(result);
        }

        [Authorize(Roles = "RESTAURANT_MANAGER")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var context = new ZderkoDbContext();
            Dish result = context.Dishes.Find(id);
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if (result.Restaurant != null)
            {
                if(result.Restaurant.UserId != userId)
                {
                    return Json(new { result = "failure" });
                }
            }

            context.Entry(result).State = System.Data.Entity.EntityState.Deleted;

            context.SaveChanges();

            context.Dispose();

            return Json(new { result = "success" });
        }

        private void FillDropDownValues()
        {
            var context = new ZderkoDbContext();
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            IEnumerable<Restaurant> possibleRestaurants = context.Restaurants
                .ToList().Where(r => r.UserId == userId);

            var selectItems = new List<System.Web.Mvc.SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var restaurant in possibleRestaurants)
            {
                listItem = new SelectListItem();
                listItem.Text = restaurant.Name;
                listItem.Value = restaurant.ID.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
            ViewBag.PossibleRestaurants = selectItems;
        }

        [Authorize]
        [HttpGet]
        public ActionResult IndexAjax()
        {
            var context = new ZderkoDbContext();

            IEnumerable<Dish> dataQuery = context.Dishes.Include(d => d.Restaurant).ToList();

            //context.Dispose();
            return PartialView("_IndexTable", dataQuery.ToList());
        }

    }
}