using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityRentalIncomeDetailDao : EntityDao<RentalIncomeDetail>, IRentalIncomeDetailDao
    {
        public EntityRentalIncomeDetailDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
