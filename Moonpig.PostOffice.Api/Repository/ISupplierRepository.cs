using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api.Repository
{
    public interface ISupplierRepository
    {
        Supplier GetSupplier(int id);
    }
}
