using LMate.BusinessObjects;
using System.Data.Entity;

namespace LMate.DataObjects.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<DepreciationAsset> DepreciationAssets { get; set; }
        public DbSet<DepreciationBuilding> DepreciationBuildings { get; set; }
        public DbSet<Disposal> Disposals { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<RentalIncomeDetail> RentalIncomeDetails { get; set; }
        public DbSet<TaxUser> TaxUsers { get; set; }

        public EFDbContext() : base("DefaultConnection"){}
    }
}
