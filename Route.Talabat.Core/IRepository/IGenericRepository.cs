﻿using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Specificatioons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.IRepository
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
		Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
	}
}
