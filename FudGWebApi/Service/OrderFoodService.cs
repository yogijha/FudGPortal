using FudGWebApi.Data;
using FudGWebApi.IService;
using FudGWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGWebApi.Service
{
    public class OrderFoodService : IOrderFoodService
    {
        FudGPortalContext _db;
        public OrderFoodService(FudGPortalContext db)
        {
            _db = db;
        }
        public async Task<int> AddOrderFood(OrderFood orderFood)
        {
            if (_db != null)
            {
                await _db.OrderFoods.AddAsync(orderFood);
                await _db.SaveChangesAsync();
                var order = _db.OrderFoods.FirstOrDefault(x => x.Customerid == orderFood.Customerid);
                if (order != null)
                    return order.Customerid;
                return 0;
            }
            return 0;
        }
    }
}
