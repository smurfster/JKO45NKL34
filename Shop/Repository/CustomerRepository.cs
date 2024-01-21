using Domain.Entities.Customer;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<CustomerEntity> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
