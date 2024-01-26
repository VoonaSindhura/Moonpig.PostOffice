using Moonpig.PostOffice.Data;
using System.Linq;

namespace Moonpig.PostOffice.Api.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IDbContext _dbContext;
        public SupplierRepository(IDbContext dbContext) { _dbContext = dbContext; }

        public Supplier GetSupplier(int id)
        {
            return _dbContext.Suppliers.SingleOrDefault(x => x.SupplierId == id);
        }
    }
}
