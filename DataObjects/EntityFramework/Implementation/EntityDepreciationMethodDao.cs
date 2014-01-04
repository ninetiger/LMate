using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityDepreciationMethodDao : EntityDao<DepreciationMethod>, IDepreciationMethodDao
    {
        public EntityDepreciationMethodDao(LMateEntities context)
            : base(context)
        {
        }

    }
}
