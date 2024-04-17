using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Repository.Data.Config
{
	public class ProductCategoryConfigrations : IEntityTypeConfiguration<ProductCategory>
	{
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder.Property(c => c.Name).IsRequired();
		}
	}
}
