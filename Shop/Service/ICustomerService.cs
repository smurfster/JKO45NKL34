using Models;

namespace Service
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer (CreateUpdateCustomerRequestModel createCustomerRequestModel);
        Task<bool> Delete(int id);
        Task<GetCustomerResponseModel> GetCustomer(int id);
        Task<bool> Update(int id, CreateUpdateCustomerRequestModel createCustomerRequestModel);
    }
}
