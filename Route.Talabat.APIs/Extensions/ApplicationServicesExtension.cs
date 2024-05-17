using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helper;
using Route.Talabat.Core.IRepository;
using Route.Talabat.Repository.BasketRepo;
using Route.Talabat.Repository.GenericRepo;

namespace Route.Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplictionServices(this IServiceCollection services)
		{

			services.AddScoped(typeof(IBasketRepo),typeof(BasketRepo));
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductBrand>,GenericRepository<ProductBrand>>();
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductCategory>,GenericRepository<ProductCategory>>();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			//webApplicationBuilder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
			services.AddAutoMapper(typeof(MappingProfiles));
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors).Select(e => e.ErrorMessage).ToList();
					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};

			});

			return services;
		}
	}
}
