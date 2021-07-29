using System;
using System.Collections.Generic;

#nullable disable

namespace FudGWebApi.Models
{
    public partial class OrderFood
    {
        public int Foodid { get; set; }
        public int Customerid { get; set; }
        public DateTime Orderdate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Food Food { get; set; }
    }
}
