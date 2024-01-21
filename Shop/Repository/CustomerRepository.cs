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

        public async Task<CustomerEntity> GetCustomerById(int id)
        {
            var result = await dbContext.Customers.SingleOrDefaultAsync(o => o.Id == id);
            return result;
        }

        public async Task UpdateCustomer(CustomerEntity customerEntity)
        {
            throw new NotImplementedException();
        }
    }
}
