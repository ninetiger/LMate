using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityDepreciationAssetDao : EntityDao<DepreciationAsset>, IDepreciationAssetDao
    {
        public EntityDepreciationAssetDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
