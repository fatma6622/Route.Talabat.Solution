﻿using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specificatioons
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? Criteria { get; set; } = null;
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; } = null;
		public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

		public BaseSpecifications()
        {
			//Includes = new List<Expression<Func<T, object>>>();

		}
		public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
		{
			Criteria = CriteriaExpression;

			//Includes = new List<Expression<Func<T, object>>>();

		}
		public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
		{
			OrderBy=orderByExpression;
		}
		public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
		{
			OrderByDesc = orderByDescExpression;
		}
	}
}
