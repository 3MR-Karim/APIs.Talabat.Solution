using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.RepositoriesContract;

namespace Talabat.Solution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiControlller
    {
        private readonly IGenericRepository<Product> _productRepo;

        // between apicontroller VS common conteroer
        // base consiat base every api contorller
        // commom has end endpoint use endpoint common in every controller  


        public ProductsController( IGenericRepository<Product> productRepo)
        {
            this._productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {


            var products = await _productRepo.GetAllAsync();
           return Ok(products);
            
            ;


        }













    }
}
