using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityDisposalTypeDao : EntityDao<DisposalType>, IDisposalTypeDao
    {
        public EntityDisposalTypeDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
