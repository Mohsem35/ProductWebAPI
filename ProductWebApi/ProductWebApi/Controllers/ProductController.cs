using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductWebApi.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok(_db.Products.ToList());
        }

           
        [HttpPost]
        public IHttpActionResult Create([FromBody] Product product)
        {
            _db.Products.Add(product);
            int rowCount = _db.SaveChanges();
            if (rowCount > 0)
            {
                return Ok("Product has been saved");
            }
            return BadRequest("Failed");
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var products = _db.Products.ToList();
            if (products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var product = _db.Products.FirstOrDefault(f => f.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Update product in database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update([FromBody] Product product)
        {
            //Product p = _db.Products.Where(s => s.Id == product.Id).FirstOrDefault(); 
            if(product.Id <= 0)
            {
                return NotFound();
            }
            _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            var rowCount = _db.SaveChanges();
            if (rowCount > 0)
            {
                return Ok("Product has been modified");
            }
            return BadRequest("Failed");            
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(f => f.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            var rowCount = _db.SaveChanges();
            if (rowCount > 0)
            {
                return Ok("Product has been deleted");
            }
            return BadRequest("Failed"); 
        }
    }
}
