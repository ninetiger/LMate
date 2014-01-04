using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityTaxUserDao : EntityDao<TaxUser>, ITaxUserDao
    {
        public EntityTaxUserDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
