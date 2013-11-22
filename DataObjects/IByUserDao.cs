using System.Linq;

namespace DataObjects
{
    public interface IByUserDao<T> : IDao<T> where T : class
    {
        IQueryable<T> GetAllByUserId(string userId, string sortExpression = "Id ASC");
    }
}
