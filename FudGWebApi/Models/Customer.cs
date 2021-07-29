using System;
using System.Collections.Generic;

#nullable disable

namespace FudGWebApi.Models
{
    public partial class Customer
    {
        public Customer()
        {
            OrderFoods = new HashSet<OrderFood>();
        }

        public int Customerid { get; set; }
        public string Customername { get; set; }
        public string Customeraddress { get; set; }
        public long Phonenumber { get; set; }
        public string Emailid { get; set; }
        public string Password { get; set; }

        public virtual ICollection<OrderFood> OrderFoods { get; set; }
    }
}
