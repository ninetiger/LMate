using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityVendorDao : EntityDao<Vendor>, IVendorDao
    {
        public EntityVendorDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
