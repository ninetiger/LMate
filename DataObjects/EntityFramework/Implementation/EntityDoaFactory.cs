using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using LMate.DataObjects;

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
            get { return new EntityReceiptDao(); }
        }

        public IReceiptTypeDao ReceiptTypeDao
        {
            get { return new EntityReceiptTypeDao(); }
        }
    }

}
