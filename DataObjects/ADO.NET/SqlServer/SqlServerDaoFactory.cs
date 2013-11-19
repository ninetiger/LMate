using LMate.DataObjects;

namespace DataObjects.ADO.NET.SqlServer
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
        /// Gets a Sql server specific receipt data access object.
        /// </summary>
        public IReceiptDao ReceiptDao { get { return new SqlServerReceiptDao(); } }

        public IAccountTypeDao AccountTypeDao
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
