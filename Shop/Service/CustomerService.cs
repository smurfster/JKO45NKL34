using Models;
using Repository;
using Service.Mappers;

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
            var result = await customerRepository.GetCustomerById(id);

            return result.CustomerEntityToGetCustomerResponseModel();
        }
    }
}
