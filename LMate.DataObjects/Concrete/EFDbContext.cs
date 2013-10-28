using LMate.BusinessObjects;
using System.Data.Entity;

namespace LMate.DataObjects.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<AssetDepreciation> AssetDepreciations { get; set; }
        public DbSet<BuildingDepreciation> BuildingDepreciations { get; set; }
        public DbSet<Disposal> Disposals { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<RentalIncome> RentalIncomes { get; set; }

        public EFDbContext() : base("DefaultConnection"){}
    }
}
