using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;

namespace LMate.DataObjects.Concrete
{
    public class EFAssetDepreciationRepository : IAssetDepreciationRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<AssetDepreciation> AssetDepreciations
        {
            get { return _context.AssetDepreciations; }
        }
    }
}
