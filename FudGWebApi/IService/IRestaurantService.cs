using FudGWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGWebApi.IService
{
    public interface IRestaurantService
    {
        public Restaurant RegisterRestuarant(Restaurant restaurant);
        public Restaurant ValidateRestaurant(string Resemail, string Respassword);
        public Food AddFood(Food food);
        public bool UpdateFood(double Price, int Foodid);
        public List<Restaurant> GetAllRestaurant();
        public List<Food> GetFood(int id);
        public bool DeleteRestaurant(int id);
    }
}
