using Models;

namespace Service
{
    public interface IProductService
    {
        Task<int> CreateProduct(CreateUpdateProductRequestModel model);
        Task<GetProductResponseModel> GetProduct(int id);
        Task<bool> Update(int id, CreateUpdateProductRequestModel model);
    }
}
