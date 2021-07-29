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
    public class OrderFoodController : ControllerBase
    {

        IOrderFoodService _Iservice;
        public OrderFoodController(IOrderFoodService Iservice)
        {
            _Iservice = Iservice;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddOrderFood([FromBody] OrderFood orderFood)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var custId = await _Iservice.AddOrderFood(orderFood);
                    if (custId > 0)
                    {
                        return Ok("Order placed");
                    }
                    else
                    {
                        return NotFound("Already placed the order");
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
