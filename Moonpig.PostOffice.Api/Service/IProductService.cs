using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api.Service
{
    public interface IProductService
    {
        Product GetProduct(int productId);
    }
}
