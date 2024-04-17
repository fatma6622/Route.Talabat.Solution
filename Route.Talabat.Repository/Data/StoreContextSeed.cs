using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Repository.Data
{
	public class StoreContextSeed
	{
		public async static Task seedAsync(StoreContext _dbcontext)
		{
			if (_dbcontext.ProductBrands.Count()==0)
			{
				var brandData = File.ReadAllText("../Route.Talabat.Repository/Data/DataSeed/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
				if (brands?.Count() > 0)
				{
					foreach (var brand in brands)
					{
						_dbcontext.Set<ProductBrand>().Add(brand);
					}
					await _dbcontext.SaveChangesAsync();
				} 
			}
			if (_dbcontext.ProductCategorys.Count() == 0)
			{
				var categoryData = File.ReadAllText("../Route.Talabat.Repository/Data/DataSeed/categories.json");
				var categorys = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);
				if (categorys?.Count() > 0)
				{
					foreach (var category in categorys)
					{
						_dbcontext.Set<ProductCategory>().Add(category);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}
			if (_dbcontext.Products.Count() == 0)
			{
				var productData = File.ReadAllText("../Route.Talabat.Repository/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);
				if (products?.Count() > 0)
				{
					foreach (var product in products)
					{
						_dbcontext.Set<Product>().Add(product);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}
		}
	}
}
