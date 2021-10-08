using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrudService.EntityClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace CrudService.Controllers
{
    [ApiController]
    [Route("api/Home")]
    [EnableCors("AllowOrigin")]
    public class HomeController : ControllerBase
    {
        private IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            DbService.connectionString = configuration.GetConnectionString("LocalConnection");
        }

        [HttpPost]
        [Route("AddProduct")]
        public bool AddProduct([FromBody] Product product)
        {
            var isProductSaved = DbService.AddProduct(product);
            return isProductSaved;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public List<Product> GetAllProduct()
        {
            var productList = DbService.GetAllProduct();
            return productList;
        }
        [HttpPut]
        [Route("UpdateProduct")]
        public bool UpdateProduct([FromBody] Product product)
        {
            var isProductUpdated = DbService.UpdateProduct(product);
            return isProductUpdated;
        }
        [HttpDelete]
        [Route("DeleteProduct/{productId}")]
        public bool DeleteProduct(int productId)
        {
            var isProductDeleted = DbService.DeleteProduct(productId);
            return isProductDeleted;
        }


    }
}
