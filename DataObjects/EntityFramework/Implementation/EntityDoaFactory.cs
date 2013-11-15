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

        ///// <summary>
        ///// Gets a Entity Framework specific order data access object.
        ///// </summary>
        //public IOrderDao OrderDao
        //{
        //    get { return new EntityOrderDao(); }
        //}

        ///// <summary>
        ///// Gets a Entity Framework specific order detail data access object.
        ///// </summary>
        //public IOrderDetailDao OrderDetailDao
        //{
        //    get { return new EntityOrderDetailDao(); }
        //}

        ///// <summary>
        ///// Gets a Entity Framework specific product data access object.
        ///// </summary>
        //public IProductDao ProductDao
        //{
        //    get { return new EntityProductDao(); }
        //}

        ///// <summary>
        ///// Gets a Entity Framework specific category data access object.
        ///// </summary>
        //public ICategoryDao CategoryDao
        //{
        //    get { return new EntityCategoryDao(); }
        //}
    }
}
