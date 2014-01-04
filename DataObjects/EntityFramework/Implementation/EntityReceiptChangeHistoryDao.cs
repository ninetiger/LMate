using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityReceiptChangeHistoryDao : EntityDao<ReceiptChangeHistory>, IReceiptChangeHistoryDao
    {
        public EntityReceiptChangeHistoryDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
