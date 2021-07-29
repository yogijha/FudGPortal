using FudGWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGWebApi.IService
{
    public interface ICustomerService
    {
        public Task<int> AddCustomer(Customer customer);
        public Task<int> UpdatePassword(int id, string password);
    }
}
