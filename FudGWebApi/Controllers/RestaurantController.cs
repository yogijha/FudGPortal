using FudGWebApi.IService;
using FudGWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _Iservice;
        public RestaurantController(IRestaurantService Iservice)
        {
            _Iservice = Iservice;
        }
        [HttpPost("AddRestuarant")]
        //[Authorize(Roles = "Restaurant")]
        public IActionResult AddRestuarant(Restaurant restaurant)
        {
            try
            {
                var val = _Iservice.RegisterRestuarant(restaurant);
                if (val != null)
                    return Ok("Registered Successfully");
                else return BadRequest("Could Not Register");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("AddFood")]
        [Authorize(Roles = "Restaurant")]
        public IActionResult AddFoodDetail(Food food)
        {
            try
            {
                var val = _Iservice.AddFood(food);
                if (val != null)
                    return Ok("Added");
                else return BadRequest("Could not Add");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("VerifyRestaurant")]
        public Restaurant VerifyUser(string email, string password)
        {
            try
            {
                return _Iservice.ValidateRestaurant(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Restaurant")]
        public IActionResult UpdateFoodDetails(double price, int foodid)
        {
            try
            {
                var value = _Iservice.UpdateFood(price, foodid);
                if (value)
                    return Ok("Price updated");
                else
                    return NotFound("Id not present");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Restaurant,Customer")]
        public IActionResult GetAllRestaurant()
        {
            try
            {
                var value = _Iservice.GetAllRestaurant();
                return Ok(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Restaurant,Customer,Admin")]
        public IActionResult GetFood(int id)
        {
            try
            {
                var value = _Iservice.GetFood(id);
                return Ok(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteRestaurant(int id)
        {
            try
            {
                var value = _Iservice.DeleteRestaurant(id);
                if (value)
                    return Ok("deleted");
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
