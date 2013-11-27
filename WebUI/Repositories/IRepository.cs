using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity: class 
    {
        Task<IQueryable<TEntity>> GetAllByUserIdAsync(string userId);

        void Insert(TEntity entityToInsert);

        void Update(TEntity entityToUpdate);

        Task DeleteAsync(int id);

        Task<int> SaveChangesAsync();
    }
}
