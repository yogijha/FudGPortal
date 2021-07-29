using FudGWebApi.Data;
using FudGWebApi.IService;
using FudGWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudGWebApi.Service
{
    public class CustomerService : ICustomerService
    {
        FudGPortalContext _db;
        public CustomerService(FudGPortalContext db)
        {
            _db = db;
        }
        public async Task<int> AddCustomer(Customer customer)
        {
            if (_db != null)
            {
                await _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();

                return customer.Customerid;
            }
            return 0;
        }

        public async Task<int> UpdatePassword(int id, string password)
        {
            if (_db != null)
            {
                var cust = _db.Customers.FirstOrDefault(x => x.Customerid == id);

                if (cust != null)
                {
                    cust.Password = password;
                    _db.Customers.Update(cust);
                    await _db.SaveChangesAsync();

                    return cust.Customerid;
                }
                
            }
            return 0;
        }
    }
}
