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
    public class CustomerController : ControllerBase
    {
        ICustomerService _Iservice;
        public CustomerController(ICustomerService Iservice)
        {
            _Iservice = Iservice;
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var custId = await _Iservice.AddCustomer(customer);
                    if (custId > 0)
                    {
                        return Ok("Registered successfully");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }



        [HttpPut("{id}/{password}")]
        //[Route("UpdatePassword")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdatePassword(int id, string password)
        {
            try
            {
                var result = await _Iservice.UpdatePassword(id, password);
                if (result != 0)
                {
                    return Ok("Updated");
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }



        }
    }
}
