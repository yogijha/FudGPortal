using FudGWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGWebApi.IService
{
    public interface IOrderFoodService
    {
        public Task<int> AddOrderFood(OrderFood orderFood);
    }
}
