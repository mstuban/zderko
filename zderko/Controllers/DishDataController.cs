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
    [RoutePrefix("api/DishData")]
    public class DishDataController : ApiController
    {

        // GET: api/DishData
        public IEnumerable<Dish> Get()
        {
            var context = new ZderkoDbContext();
            return context.Dishes.ToList();
        }

        // GET: api/DishData/5
        public Dish Get(int id)
        {
            var context = new ZderkoDbContext();
            return context.Dishes.Find(id);
        }

        [HttpGet]
        [Route("Pretraga/{q}")]
        public IEnumerable<Dish> Pretraga(string q)
        {
            var context = new ZderkoDbContext();
            return context.Dishes.ToList().Where(p => p.Name.Contains(q)).ToList();
        }

        // POST: api/DishData
        public void Post([FromBody]Dish data)
        {
            var context = new ZderkoDbContext();
            context.Dishes.AddOrUpdate(data);
            context.SaveChanges();
            context.Dispose();
        }

        // PUT: api/DishData/5
        public void Put(int id, [FromBody]Dish data)
        {
            var context = new ZderkoDbContext();
            context.Dishes.AddOrUpdate(data);
            context.SaveChanges();
            context.Dispose();
        }

        // DELETE: api/DishData/5
        public void Delete(int id)
        {
            var context = new ZderkoDbContext();
            var result = context.Dishes.Find(id);

            if (result != null)
            {
                context.Entry(result).State = System.Data.Entity.EntityState.Deleted;
            }
            context.SaveChanges();
            context.Dispose();
        }
    }
}
