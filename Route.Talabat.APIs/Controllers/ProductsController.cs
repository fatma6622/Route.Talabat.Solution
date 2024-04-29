using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Dtos;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;
using Route.Talabat.Core.Specificatioons;
using Route.Talabat.Core.Specificatioons.ProductSpecs;

namespace Route.Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IGenericRepository<ProductCategory> _categoryRepo;
		private readonly IGenericRepository<ProductBrand> _brandRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductCategory> categoryRepo,
			IGenericRepository<ProductBrand> brandRepo, IMapper mapper)
        {
			_productsRepo = productsRepo;
			_categoryRepo = categoryRepo;
			_brandRepo = brandRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParam specParam)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(specParam);
			var products=await _productsRepo.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IEnumerable<Product>, IReadOnlyList<ProductToReturnDto>>(products));
		}
		[ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
		[HttpGet("{Id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int Id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(Id);
			var product = await _productsRepo.GetWithSpecAsync(spec);
			if(product == null)
				return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			
			var brands = await _brandRepo.GetAllAsync();
			return Ok(brands);
		}
		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{

			var categories = await _categoryRepo.GetAllAsync();
			return Ok(categories);
		}
	}
}
