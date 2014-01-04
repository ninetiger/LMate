using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityAccountTypeDao : EntityDao<AccountType>, IAccountTypeDao
    {
        public EntityAccountTypeDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
