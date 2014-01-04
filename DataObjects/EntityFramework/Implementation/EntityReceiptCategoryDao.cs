using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityReceiptCategoryDao : EntityDao<ReceiptCategory>, IReceiptCategoryDao
    {
        public EntityReceiptCategoryDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
