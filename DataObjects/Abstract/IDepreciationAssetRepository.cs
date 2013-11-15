using System.Linq;
using LMate.BusinessObjects;

namespace DataObjects.Abstract
{
    public interface IDepreciationAssetRepository
    {
        IQueryable<DepreciationAsset> DepreciationAssets { get; }
    }
}
