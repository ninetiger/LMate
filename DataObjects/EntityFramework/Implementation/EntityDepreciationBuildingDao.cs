using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityDepreciationBuildingDao : EntityDao<DepreciationBuilding>, IDepreciationBuildingDao
    {
        public EntityDepreciationBuildingDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
