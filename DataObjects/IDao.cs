using System.Threading.Tasks;

namespace DataObjects
{
    public interface IDao<in T> where T : class
    {
        Task SaveAsync(T bo);

        Task DeleteAsync(T bo);
    }
}
