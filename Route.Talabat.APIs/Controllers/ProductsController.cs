using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;

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
			var products=await _productsRepo.GetAllAsync();
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
