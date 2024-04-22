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
        public ProductWithBrandAndCategorySpecifications():base()
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p=>p.Category);
		}
    }
}
