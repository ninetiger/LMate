using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityDisposalDao : EntityDao<Disposal>, IDisposalDao
    {
        public EntityDisposalDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
