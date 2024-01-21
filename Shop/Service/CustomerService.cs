using Domain.Persistence;
using Models;
using Repository;
using Service.Mappers;
using System.Runtime.InteropServices;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly EFContext dbContext;

        public CustomerService(ICustomerRepository customerRepository, EFContext dbContext)
        {
            this.customerRepository = customerRepository;
            this.dbContext = dbContext;
        }

        public async Task<int> CreateCustomer (CreateUpdateCustomerRequestModel createCustomerRequestModel)
        {     
            var entity = createCustomerRequestModel.CreateCustomerRequestModelToCustomerEntity();
            var result = await customerRepository.CreateCustomer(entity);
            dbContext.SaveChanges();
            return result.Id;
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

        public async Task Update(CreateUpdateCustomerRequestModel createCustomerRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
