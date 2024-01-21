using Models;

namespace Service
{
    public interface ICustomerService
    {
        Task CreateCustomer (CreateCustomerRequestModel createCustomerRequestModel);
        Task<GetCustomerResponseModel> GetCustomer(int id);
    }
}
