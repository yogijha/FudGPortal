using FudGWebApi.Data;
using FudGWebApi.IService;
using FudGWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGWebApi.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly FudGPortalContext _fudGPortalContext;

        public RestaurantService()
        {
        }

        public RestaurantService(FudGPortalContext fudGPortalContext)
        {
            _fudGPortalContext = fudGPortalContext;
        }
        public Food AddFood(Food food)
        {
            if (_fudGPortalContext != null)
            {
                _fudGPortalContext.Foods.Add(food);
                _fudGPortalContext.SaveChanges();
                return food;
            }
            else return null;
        }

        public bool DeleteRestaurant(int id)
        {

            if (_fudGPortalContext != null)
            {
                //find food with restaurant id=id
                var food = (from fc in _fudGPortalContext.Foods
                            where fc.Resid == id 
                            select new Food
                            {
                                Foodid = fc.Foodid,
                                Title = fc.Description,
                                Description = fc.Description,
                                Price = fc.Price,
                                Resid=fc.Resid
                            }).ToList();

                if (food.Count > 0)
                {
                    for(int i = 0; i < food.Count; i++)
                    {
                        var orderfood = (from fc in _fudGPortalContext.OrderFoods
                                         where fc.Foodid == food[i].Foodid
                                         select new OrderFood
                                         {
                                             Foodid = food[i].Foodid,
                                             Customerid = fc.Customerid,
                                             Orderdate = fc.Orderdate

                                         }).ToList();

                        if (orderfood != null)
                        {
                            while(orderfood.Count>0)
                            {
                                _fudGPortalContext.OrderFoods.Remove(orderfood[0]);
                                _fudGPortalContext.SaveChangesAsync();
                            }
                        }

                    }

                    while(food.Count>0)
                    {
                        _fudGPortalContext.Foods.Remove(food[0]);
                        _fudGPortalContext.SaveChangesAsync();
                    }
                }
                
                    //delete restaurant
                    var value = _fudGPortalContext.Restaurants.FirstOrDefault(r => r.Resid == id);
                    if (value != null)
                    {
                        _fudGPortalContext.Restaurants.Remove(value);
                        _fudGPortalContext.SaveChanges();
                        return true;
                    }
                    return false;
                



               /* //delete orderfood
                if (food.Count != 0)
                {
                    var orderfood = (from fc in _fudGPortalContext.OrderFoods
                                     where fc.Foodid == food[0].Foodid
                                     select new OrderFood
                                     {
                                         Foodid = food[0].Foodid,
                                         Customerid = fc.Customerid,
                                         Orderdate = fc.Orderdate

                                     }).ToList();



                    if (orderfood != null)
                    {
                        for (int j = 0; j < orderfood.Count; j++)
                        {
                            _fudGPortalContext.OrderFoods.Remove(orderfood[j]);
                            _fudGPortalContext.SaveChangesAsync();
                        }
                    }

                    _fudGPortalContext.Foods.Remove(food[0]);
                    _fudGPortalContext.SaveChangesAsync();
                    //orderfood = null;
                }

                


                //delete restaurant

                var value = _fudGPortalContext.Restaurants.FirstOrDefault(r => r.Resid == id);
                if (value != null)
                {
                    _fudGPortalContext.Remove(value);
                    _fudGPortalContext.SaveChanges();
                    return true;
                }
                return false; */
            }
            else return false; 
               
        }

        public List<Restaurant> GetAllRestaurant()
        {
            if (_fudGPortalContext != null)
            {
                return _fudGPortalContext.Restaurants.ToList();
            }
            else return null;
        }
        public List<Food> GetFood(int id)
        {
            if (_fudGPortalContext != null)
            {
                return (from f in _fudGPortalContext.Foods
                        where f.Resid == id
                        select new Food
                        {
                            Foodid = f.Foodid,
                            Title = f.Title,
                            Description = f.Description,
                            Price = f.Price,
                            Resid = f.Resid
                        }).ToList();
            }
            return null;
        }

        public Restaurant RegisterRestuarant(Restaurant restaurant)
        {
            if (_fudGPortalContext != null)
            {
                _fudGPortalContext.Restaurants.Add(restaurant);
                _fudGPortalContext.SaveChanges();
                return restaurant;
            }
            else return null;
        }

        public bool UpdateFood(double Price, int Foodid)
        {
            if (_fudGPortalContext != null)
            {
                Food food = _fudGPortalContext.Foods.Find(Foodid);
                if (food != null)
                {
                    food.Price = Price;
                    _fudGPortalContext.SaveChanges();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public Restaurant ValidateRestaurant(string Resemail, string Respassword)
        {
            Restaurant restaurant = _fudGPortalContext.Restaurants.Where(x => x.Email == Resemail).FirstOrDefault();
            if (restaurant != null)
            {
                if (restaurant.Respassword == Respassword)
                {
                    return restaurant;
                }
            }
            throw new Exception($"Restaurant not found!!");
        }
    }
}
