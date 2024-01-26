using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api.Repository
{
    public interface IProductRepository
    {
        Product GetProduct(int productId);
    }
}
