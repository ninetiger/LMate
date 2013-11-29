using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity: class 
    {
        Task<IEnumerable<TEntity>> GetAllByUserIdAsync(string userId);

        void Insert(TEntity entityToInsert);

        Task Update(TEntity entityToUpdate);

        Task DeleteAsync(int id);

        Task<int> SaveChangesAsync();
    }
}
