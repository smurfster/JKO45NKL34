using Models;

namespace Service
{
    public interface IProductService
    {
        Task<int> CreateProduct(CreateUpdateProductRequestModel model);
        Task<GetProductResponseModel> GetProduct(int id);
        void Update(int id, CreateUpdateProductRequestModel model);
    }
}
