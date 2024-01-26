using Moonpig.PostOffice.Api.Repository;
using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository) { _supplierRepository = supplierRepository; }

        public Supplier GetSupplier(int id)
        {
            return _supplierRepository.GetSupplier(id);
        }
    }
}
