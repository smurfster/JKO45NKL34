using Domain.Entities.Customer;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<CustomerEntity> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
