using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api.Service
{
    public interface ISupplierService
    {
        Supplier GetSupplier(int id);
    }
}
