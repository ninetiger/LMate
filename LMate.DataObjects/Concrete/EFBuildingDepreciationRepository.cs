﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;

namespace LMate.DataObjects.Concrete
{
    public class EFBuildingDepreciationRepository : IBuildingDepreciationRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<BuildingDepreciation> BuildingDepreciations
        {
            get { return _context.BuildingDepreciations; }
        }
    }
}
