using Models;

namespace Service
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer (CreateCustomerRequestModel createCustomerRequestModel);
        Task<GetCustomerResponseModel> GetCustomer(int id);
    }
}
