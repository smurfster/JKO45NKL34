using Models;

namespace Service
{
    public interface ICustomerService
    {
        Task<GetCustomerResponseModel> GetCustomer(int id);
    }
}
