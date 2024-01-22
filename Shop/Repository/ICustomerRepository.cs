
using Domain.Entities.Customer;

namespace Repository
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> CreateCustomer(CustomerEntity customerEntity);
        Task<bool> DeleteCustomer(int id);
        Task<CustomerEntity?> GetCustomerById(int id);
        Task<bool> UpdateCustomer(int id, CustomerEntity customerEntity);
    }
}
