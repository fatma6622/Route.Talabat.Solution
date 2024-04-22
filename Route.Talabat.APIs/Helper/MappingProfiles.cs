using AutoMapper;
using Route.Talabat.APIs.Dtos;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.APIs.Helper
{
	public class MappingProfiles:Profile
	{
		private readonly IConfiguration _configuration;

		public MappingProfiles(IConfiguration configuration)
        {
			_configuration = configuration;
			CreateMap<Product,ProductToReturnDto>()
                .ForMember(d=>d.Brand,o=>o.MapFrom(s=>s.Brand.Name))
                .ForMember(d=>d.Category,o=>o.MapFrom(s=>s.Category.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
		}
    }
}
