using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;
using Route.Talabat.Core.Specificatioons;
using Route.Talabat.Core.Specificatioons.ProductSpecs;

namespace Route.Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;

		public ProductsController(IGenericRepository<Product> productsRepo)
        {
			_productsRepo = productsRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
			var products=await _productsRepo.GetAllWithSpecAsync(spec);
			return Ok(products);
		}
		[HttpGet("{Id}")]
		public async Task<ActionResult<Product>> GetProduct(int Id)
		{
			var product = await _productsRepo.GetAsync(Id);
			if(product == null)
				return NotFound();
			return Ok(product);
		}
	}
}
