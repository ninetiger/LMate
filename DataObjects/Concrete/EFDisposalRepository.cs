using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;

namespace LMate.DataObjects.Concrete
{
    public class EFDisposalRepository : IDisposalRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<Disposal> Disposals
        {
            get { return _context.Disposals; }
        }
    }
}
