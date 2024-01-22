using Domain.Entities.Customer;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EFContext dbContext;

        public CustomerRepository(EFContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CustomerEntity> CreateCustomer(CustomerEntity customerEntity)
        {
            var result = await dbContext.Customers.AddAsync(customerEntity);
            return result.Entity;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            
            if (customer == null) { return false; }
            
            dbContext.Customers.Remove(customer);
            return true;
        }

        public async Task<CustomerEntity?> GetCustomerById(int id)
        {
            var result = await dbContext.Customers.SingleOrDefaultAsync(o => o.Id == id);
            return result;
        }

        public async Task<bool> UpdateCustomer(int id, CustomerEntity customerEntity)
        {
            var customer = await dbContext.Customers.FindAsync(id);

            if (customer == null) return false;

            customer.UpdatePhone(customerEntity.Phone);
            customer.UpdateEmail(customerEntity.Email);
            customer.UpdateName(customerEntity.Name);

            return true;
        }
    }
}
