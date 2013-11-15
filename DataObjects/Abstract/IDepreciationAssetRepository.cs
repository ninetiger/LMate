using System.Linq;
using LMate.BusinessObjects;

namespace LMate.DataObjects.Abstract
{
    public interface IDepreciationAssetRepository
    {
        IQueryable<DepreciationAsset> DepreciationAssets { get; }
    }
}
