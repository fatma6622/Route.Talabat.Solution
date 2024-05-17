using Route.Talabat.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.Dtos
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; } = null!;
		[Required]
		public List<BasketItemDto> Items { get; set; } = null!;
	}
}
