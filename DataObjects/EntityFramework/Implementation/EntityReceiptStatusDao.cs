namespace DataObjects.EntityFramework.Implementation
{
    public class EntityReceiptStatusDao : EntityDao<ReceiptStatus>, IReceiptStatusDao
    {
        public EntityReceiptStatusDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
