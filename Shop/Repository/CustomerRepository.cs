using Domain.Entities.Customer;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IEFContext dbContext;

        public CustomerRepository(IEFContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateCustomer(CustomerEntity customerEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerEntity> GetCustomerById(int id)
        {
            var result = await dbContext.Customers.SingleOrDefaultAsync(o => o.Id == id);
            return result;
        }
    }
}
