using System;
using System.Collections.Generic;

#nullable disable

namespace FudGWebApi.Models
{
    public partial class Food
    {
        public Food()
        {
            OrderFoods = new HashSet<OrderFood>();
        }

        public int Foodid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? Resid { get; set; }

        public virtual Restaurant Res { get; set; }
        public virtual ICollection<OrderFood> OrderFoods { get; set; }
    }
}
