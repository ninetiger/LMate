using BusinessObjects;
using DataObjects.EntityFramework;

namespace DataObjects
{
    public interface IAspNetUserDao  : ILookupDao<AspNetUsers>
    {
    }
}
