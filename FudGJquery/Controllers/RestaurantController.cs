using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGJquery.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddFood()
        {
            return View();
        }
        public IActionResult UpdatePrice()
        {
            return View();
        }
        public IActionResult DeleteRestaurant()
        {
            return View();
        }
    }
}
