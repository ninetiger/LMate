using DataObjects.ADO.NET.SqlServer;
using DataObjects.EntityFramework.Implementation;

namespace DataObjects
{
    /// <summary>
    /// Factory of factories. This class is a factory class that creates
    /// data-base specific factories which in turn create data acces objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Factory.
    /// 
    /// This is the abstract factory design pattern applied in a hierarchy
    /// in which there is a factory of factories.
    /// </remarks>
    public class DaoFactories
    {
        /// <summary>
        /// Gets a provider specific (i.e. database specific) factory 
        /// 
        /// GoF Design Pattern: Factory
        /// </summary>
        /// <param name="dataProvider">Database provider.</param>
        /// <returns>Data access object factory.</returns>
        public static IDaoFactory GetFactory(string dataProvider)
        {
            // Return the requested DaoFactory
            switch (dataProvider)
            {
                case "EntityFramework.SqlExpress": 
                case "EntityFramework.SqlServer": return new EntityDaoFactory();
                
                //case "ADO.NET.SqlExpress":
                //case "ADO.NET.LocalDB":
                //case "ADO.NET.SqlServer": 
                default: return new SqlServerDaoFactory(); 
            }
        }
    }
}

