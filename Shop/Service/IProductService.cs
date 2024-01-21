using Models;

namespace Service
{
    public interface IProductService
    {
        Task<int> CreateProduct(CreateUpdateProductRequestModel model);
    }
}
