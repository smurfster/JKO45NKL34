
using Domain.Entities.Customer;

namespace Repository
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> GetCustomerById(int id);        
    }
}
