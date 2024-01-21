using Models;
using Repository;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<GetCustomerResponseModel> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
