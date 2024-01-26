using Moonpig.PostOffice.Api.Repository;
using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) { _productRepository = productRepository; }
        public Product GetProduct(int productId)
        {
            return _productRepository.GetProduct(productId);
        }
    }
}
