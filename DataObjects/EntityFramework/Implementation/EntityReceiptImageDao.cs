namespace DataObjects.EntityFramework.Implementation
{
    public class EntityReceiptImageDao : EntityDao<ReceiptImage>, IReceiptImageDao
    {
        public EntityReceiptImageDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
