using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityCurrencyDao : EntityDao<Currency>, ICurrencyDao
    {
        public EntityCurrencyDao(LMateEntities context) : base(context)
        {
        }
    }
}
