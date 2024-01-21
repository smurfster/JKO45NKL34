
using Domain.Entities.Customer;

namespace Repository
{
    public interface ICustomerRepository
    {
        Task CreateCustomer(CustomerEntity customerEntity);
        Task<CustomerEntity> GetCustomerById(int id);        
    }
}
