using LMate.BusinessObjects;
using System.Data.Entity;

namespace LMate.DataObjects.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Return> Returns { get; set; }

        public EFDbContext() : base("DefaultConnection"){}
    }
}
