﻿using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;
using Route.Talabat.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task<IEnumerable<T>> GetAllAsync()
		{
			if(typeof(T)==typeof(Product))
				return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p=>p.Brand).Include(p=>p.Category).ToListAsync();
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			if (typeof(T) == typeof(Product))
				return await _dbContext.Set<Product>().Where(p=>p.Id==id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}
