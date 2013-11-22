using System.Linq;
using System.Threading.Tasks;
using DataObjects.EntityFramework;

namespace DataObjects
{
    public interface ILookupDao<T> : IDao<T> where T : class
    {
        Task<AspNetUsers> GetByIdAsync(int id);

        IQueryable<T> GetAll(string sortExpression = "Id ASC");
    }
}
