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
            var entity = createCustomerRequestModel.CreateUpdateCustomerRequestModelToCustomerEntity();
            var result = await customerRepository.CreateCustomer(entity);
            dbContext.SaveChanges();
            return result.Id;
        }

        public async Task<bool> Delete(int id)
        {
            await customerRepository.DeleteCustomer(id);
            return true;
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

        public async Task<bool> Update(int id, CreateUpdateCustomerRequestModel createCustomerRequestModel)
        {
            var entity = createCustomerRequestModel.CreateUpdateCustomerRequestModelToCustomerEntity();
            var result = await customerRepository.UpdateCustomer(id, entity);
            dbContext.SaveChanges();
            return result;
        }
    }
}