using AutoMapper;
using AutoMapper.Execution;
using Route.Talabat.APIs.Dtos;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.APIs.Helper
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.PictureUrl))
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			else
				return string.Empty;
		}
	}
}
