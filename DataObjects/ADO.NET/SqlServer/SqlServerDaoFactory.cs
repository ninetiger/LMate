using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMate.DataObjects.ADO.NET.SqlServer
{
    /// <summary>
    /// Sql Server specific factory that creates Sql Server specific data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public class SqlServerDaoFactory : IDaoFactory
    {
        /// <summary>
        /// Gets a Sql server specific customer data access object.
        /// </summary>
        public IReceiptDao ReceiptDao { get { return new SqlServerReceiptDao(); } }

        ///// <summary>
        ///// Gets a Sql server specific order data access object.
        ///// </summary>
        //public IOrderDao OrderDao { get { return new SqlServerOrderDao(); } }

        ///// <summary>
        ///// Gets a Sql Server specific order detail data access object.
        ///// </summary>
        //public IOrderDetailDao OrderDetailDao { get { return new SqlServerOrderDetailDao(); } }


        ///// <summary>
        ///// Gets a Sql server specific product data access object.
        ///// </summary>
        //public IProductDao ProductDao { get { return new SqlServerProductDao(); } }

        ///// <summary>
        ///// Gets a Sql server specific category data access object.
        ///// </summary>
        //public ICategoryDao CategoryDao { get { return new SqlServerCategoryDao(); } }
    }
}
