using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataObjects.Abstract
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity: class 
    {
        Task<IEnumerable<TEntity>> GetAllByUserIdAsync(string userId);

        Task InsertAsync(TEntity entityToInsert);

        Task UpdateAsync(TEntity entityToUpdate);

        Task DeleteAsync(int id);
    }
}
