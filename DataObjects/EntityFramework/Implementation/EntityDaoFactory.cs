using System.Linq;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework specific factory that creates data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Factory.
    /// </remarks>
    public class EntityDaoFactory : IDaoFactory
    {
        /// <summary>
        /// Gets an Entity Framework specific receipt data access object.
        /// </summary>
        public IReceiptDao ReceiptDao
        {
            get { return new EntityReceiptDao(new LMateEntities()); }
        }

        //public IAccountTypeDao AccountTypeDao
        //{
        //    get { return new EntityAccountTypeDao(); }
        //}

        //public ICurrencyDao CurrencyDao
        //{
        //    get { return new EntityCurrencyDao(); }
        //}

        //public IReceiptCategoryDao ReceiptCategory
        //{
        //    get { return new EntityReceiptCategoryDao(); }
        //}

        //public IReceiptImageDao ReceiptImageDao
        //{
        //    get { return new EntityReceiptImageDao(); }
        //}

        //public IAspNetUserDao AspNetUserDao
        //{
        //    get { return new EntityAspNetUserDao(); }
        //}
    }


}
