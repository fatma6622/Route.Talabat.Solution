using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;

namespace Route.Talabat.APIs.Controllers
{
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepo _basketRepo;

		public BasketController(IBasketRepo basketRepo)
        {
			_basketRepo = basketRepo;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket=await _basketRepo.GetBasketAsync(id);
			return Ok(basket is null ? new CustomerBasket(id):basket);
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{
			var createdOrUpdatedBasket=await _basketRepo.UpdateBasketAsync(basket);
			if(createdOrUpdatedBasket is null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(createdOrUpdatedBasket);
		}
		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string id)
		{
			return await _basketRepo.DeleteBasketAsync(id);
		}
    }
}
