using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;

namespace LMate.DataObjects.Abstract
{
    public interface IBuildingDepreciationRepository
    {
        IQueryable<BuildingDepreciation> BuildingDepreciations { get; }
    }
}
