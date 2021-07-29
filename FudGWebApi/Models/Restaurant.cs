using System;
using System.Collections.Generic;

#nullable disable

namespace FudGWebApi.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Foods = new HashSet<Food>();
        }

        public int Resid { get; set; }
        public string Resname { get; set; }
        public string Resaddress { get; set; }
        public string Email { get; set; }
        public string Respassword { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
