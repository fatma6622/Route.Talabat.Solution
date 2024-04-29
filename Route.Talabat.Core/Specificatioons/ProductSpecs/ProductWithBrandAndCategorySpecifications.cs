using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specificatioons.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifications:BaseSpecifications<Product>
	{
        public ProductWithBrandAndCategorySpecifications(ProductSpecParam specParam) 
			:base(p=>
			         (string.IsNullOrEmpty(specParam.Search)||p.Name.ToLower().Contains(specParam.Search ))&&
			         (!specParam.BrandId.HasValue||p.BrandId== specParam.BrandId.Value) && 
			         (!specParam.CategoryId.HasValue||p.CategoryId== specParam.CategoryId.Value)
			)
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p=>p.Category);
			if (!string.IsNullOrEmpty(specParam.Sort))
			{
				switch(specParam.Sort)
				{
					case "priceAsc":
						AddOrderBy(p=>p.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(p => p.Price);
						break;
					default:
						AddOrderBy(p=>p.Name);
						break;
				}
			}else
				AddOrderBy(p=>p.Name);

			ApplyPagination((specParam.PageIndex - 1) * specParam.PageSize, specParam.PageSize); 
		}
		public ProductWithBrandAndCategorySpecifications(int Id) : base(p=>p.Id==Id)
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
	}
}
