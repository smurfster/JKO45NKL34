using Models;

namespace Service
{
    public interface IProductService
    {
        Task<bool> CreateProduct(CreateUpdateProductRequestModel model);
    }
}
