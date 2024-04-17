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
    }
}
