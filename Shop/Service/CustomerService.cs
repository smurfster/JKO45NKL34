using Models;
using Repository;
using Service.Mappers;
using System.Runtime.InteropServices;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task CreateCustomer (CreateCustomerRequestModel createCustomerRequestModel)
        {            
        }

        public async Task<GetCustomerResponseModel?> GetCustomer(int id)
        {
            var result = await customerRepository.GetCustomerById(id);

            if (result == null)
            {
                return null;
            }

            return result.CustomerEntityToGetCustomerResponseModel();
        }
    }
}
