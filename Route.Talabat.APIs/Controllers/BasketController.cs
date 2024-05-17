using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Dtos;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;

namespace Route.Talabat.APIs.Controllers
{
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepo _basketRepo;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepo basketRepo ,IMapper mapper )
        {
			_basketRepo = basketRepo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket=await _basketRepo.GetBasketAsync(id);
			return Ok(basket is null ? new CustomerBasket(id):basket);
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var mappedBasket=_mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
			var createdOrUpdatedBasket=await _basketRepo.UpdateBasketAsync(mappedBasket);
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
