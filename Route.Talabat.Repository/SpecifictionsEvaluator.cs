using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Specificatioons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Repository
{
	internal static class SpecifictionsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecifications<TEntity> spec)
		{
			var query = inputQuery;
			if(spec.Criteria is not null)
				query=query.Where(spec.Criteria);


			query=spec.Includes.Aggregate(query,(currentQuery,includeExpression)=>currentQuery.Include(includeExpression));



			return query;
		}
	}
}
