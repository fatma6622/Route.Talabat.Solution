﻿using Route.Talabat.Core.Entities;
using Route.Talabat.Core.IRepository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Repository
{
	public class BasketRepo : IBasketRepo
	{
		private readonly IDatabase _database;
        public BasketRepo(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{
			var basket=await _database.StringGetAsync(basketId);
			return basket.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			var createdOrUpdated=await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize( basket), TimeSpan.FromDays(30));
			if(createdOrUpdated is false) return null;
			return await GetBasketAsync(basket.Id);
		}
	}
}