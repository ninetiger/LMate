using System.Linq;
using BusinessObjects;
using DataObjects.Abstract;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;

namespace LMate.DataObjects.Concrete
{
    public class EFDepreciationAssetRepository : IDepreciationAssetRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<DepreciationAsset> DepreciationAssets
        {
            get { return _context.DepreciationAssets; }
        }
    }
}
