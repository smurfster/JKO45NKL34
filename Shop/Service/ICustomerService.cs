using Models;

namespace Service
{
    public interface ICustomerService
    {
        Task<GetUserResponseModel> GetCustomer(int id);
    }
}
