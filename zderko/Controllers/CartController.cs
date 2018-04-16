using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zderko.Models;
using System.Data.Entity;


namespace zderko.Controllers
{
    [RoutePrefix("api/Cart")]
    public class CartController : ApiController
    {

        // GET: api/Cart
        [HttpGet]
        [Route("Get/{userName}")]
        public Order Get(string userName)
            {
                var context = new ZderkoDbContext();
                
                return context.Orders.Include(o => o.User).ToList().SingleOrDefault(o => o.User.UserName == userName);
            }

        // POST: api/Cart
        [HttpPost]
        [Route("Post")]
        public void Post([FromBody]Order data)
            {
                var context = new ZderkoDbContext();
                context.Orders.AddOrUpdate(data);
                context.SaveChanges();
                context.Dispose();
        }

        // PUT: api/Cart/5
        [HttpPut]
        [Route("Put")]
        public void Put([FromBody]Order data)
            {
                var context = new ZderkoDbContext();
                context.Orders.AddOrUpdate(data);
                context.SaveChanges();
                context.Dispose();
            }

        [HttpPut]
        [Route("{userName}/AddToCart/{restaurantId}/{dishId}")]
        public void AddToCart(int dishId, string userName, int restaurantId)
        {
            var context = new ZderkoDbContext();
            var order = context.Orders.Include(o => o.User).ToList().SingleOrDefault(o => o.User.UserName == userName);

            if (order == null)
            {
                var newOrder = new Order()
                {
                    Dishes = new List<Dish>(),
                    MethodOfPayment = context.MethodsOfPayment.Find(1),
                    OrderNumber = context.Orders.Count() + 1,
                    OrderTime = DateTime.Now,
                    Restaurant = context.Restaurants.Find(restaurantId),
                    User = context.Users.ToList().SingleOrDefault(u => u.UserName == userName)
                };
                var food = context.Dishes.Find(dishId);
                newOrder.Dishes.Add(food);
                context.Orders.Add(newOrder);
            }
            else
            {
                var dish = context.Dishes.Find(dishId);
                order.Dishes.Add(dish);

                context.Orders.AddOrUpdate(order);
            }
            context.SaveChanges();
            context.Dispose();
        }

        [HttpPut]
        [Route("{userName}/RemoveFromCart/{dishId}")]
        public void RemoveFromCart(int dishId, string userName)
        {
            var context = new ZderkoDbContext();
            var order = context.Orders.Include(o => o.User).ToList().SingleOrDefault(o => o.User.UserName == userName);

            var dish = context.Dishes.Find(dishId);
            order.Dishes.Remove(dish);

            context.Orders.AddOrUpdate(order);
            context.SaveChanges();
            context.Dispose();
        }

        // DELETE: api/Cart/5
        [HttpDelete]
        [Route("Delete")]
        public void Delete(string userName)
                {
                    var context = new ZderkoDbContext();
                    var result = context.Orders.Include(o => o.User).ToList().SingleOrDefault(o => o.User.UserName == userName);

                    if (result != null)
                    {
                        context.Entry(result).State = System.Data.Entity.EntityState.Deleted;
                    }
                    context.SaveChanges();
                    context.Dispose();
                }
        }
    }
