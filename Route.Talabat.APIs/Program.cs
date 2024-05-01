using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.APIs.Helper;
using Route.Talabat.APIs.MiddleWares;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;
using Route.Talabat.Repository;
using Route.Talabat.Repository.Data;
using StackExchange.Redis;

namespace Route.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			webApplicationBuilder.Services.AddControllers();
			webApplicationBuilder.Services.AddSwaggarServices();
			webApplicationBuilder.Services.AddDbContext<StoreContext>(
				options => {
					options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
				}
			);
			webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
			{
				var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			}
			);
			webApplicationBuilder.Services.AddApplictionServices();
			//ApplicationServicesExtension.AddApplictionServices(webApplicationBuilder.Services);
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

			app.UseMiddleware<ExceptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggarMiddleware();
			}
			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
