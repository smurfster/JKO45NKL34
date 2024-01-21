using Domain.Entities.Customer;
using Domain.Persistence;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IEFContext dbContext;

        public CustomerRepository(IEFContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CustomerEntity> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
