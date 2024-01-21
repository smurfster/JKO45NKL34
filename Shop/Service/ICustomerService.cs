using Models;

namespace Service
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer (CreateUpdateCustomerRequestModel createCustomerRequestModel);
        Task<GetCustomerResponseModel> GetCustomer(int id);
        Task Update(CreateUpdateCustomerRequestModel createCustomerRequestModel);
    }
}
