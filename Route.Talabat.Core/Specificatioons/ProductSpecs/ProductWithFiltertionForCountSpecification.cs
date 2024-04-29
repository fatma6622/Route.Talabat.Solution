using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specificatioons.ProductSpecs
{
	public class ProductWithFiltertionForCountSpecification:BaseSpecifications<Product>
	{
        public ProductWithFiltertionForCountSpecification(ProductSpecParam specParam)
			: base(p =>
					 (!specParam.BrandId.HasValue || p.BrandId == specParam.BrandId.Value) &&
					 (!specParam.CategoryId.HasValue || p.CategoryId == specParam.CategoryId.Value)
			)
		{
            
        }
    }
}
