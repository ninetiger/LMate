using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.EntityFramework;

namespace WebUI.Repositories
{
    public interface IVendorRepository : IRepository<VendorViewModel>
    {
        Task<Vendor> GetVednor(string vendorName);

        Task<List<List<string>>> SearchVendorName(string searchString);

        void InsertVendor(Vendor vendor);
    }
}
