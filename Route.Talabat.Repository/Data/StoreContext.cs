﻿using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Repository.Data
{
	public class StoreContext:DbContext
	{
        public StoreContext(DbContextOptions<StoreContext> options)
			:base(options) 
		{ 

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		DbSet<Product> Products { get; set; }
		DbSet<ProductBrand> ProductBrands { get; set; }
		DbSet<ProductCategory> ProductCategorys { get; set; }
	}
}
