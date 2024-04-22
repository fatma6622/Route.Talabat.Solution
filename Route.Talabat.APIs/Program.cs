 using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Helper;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;
using Route.Talabat.Repository;
using Route.Talabat.Repository.Data;

namespace Route.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
			webApplicationBuilder.Services.AddDbContext<StoreContext>(
				options => {
					options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
				}
			);
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductBrand>,GenericRepository<ProductBrand>>();
			//webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductCategory>,GenericRepository<ProductCategory>>();
			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			webApplicationBuilder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));

			var app = webApplicationBuilder.Build();

			//Ask CLR to creating object from DbContext Explictly
			//To Avoid Using Update Database Command After Migration
			//Update Database Automatic when run Application and achive all migrations
			using var scope=app.Services.CreateScope();
			var services=scope.ServiceProvider;
			var _dbContext=services.GetRequiredService<StoreContext>();

			var loggerFactory=services.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();           //make migration
				await StoreContextSeed.seedAsync(_dbContext);       //dataSeeding
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error accured during apply migration");
			}


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
