using Moonpig.PostOffice.Data;
using System.Linq;

namespace Moonpig.PostOffice.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContext _dbContext; 
        public ProductRepository(IDbContext dbContext) { _dbContext = dbContext; }

        public Product GetProduct(int productId)
        {
            return _dbContext.Products.SingleOrDefault(x => x.ProductId == productId);
        }
    }
}
