using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IDao<TEntity> where TEntity : class
    {
        /// <param name="filter">The code Expression&lt;Func&lt;TEntity, bool>> filter means the caller will provide a lambda 
        /// expression based on the TEntity type, and this expression will return a Boolean value. 
        /// Eg, if the repository is instantiated for the Student entity type, the code in the calling 
        /// method might specify student => student.LastName == "Smith" for the filter parameter.</param>
        /// <param name="orderBy">The code Func&lt;IQueryable&lt;TEntity>, 
        /// IOrderedQueryable&lt;Entity>> orderBy also means the caller will provide a lambda expression. 
        /// But in this case, the input to the expression is an IQueryable object for the TEntity type. 
        /// The expression will return an ordered version of that IQueryable object. 
        /// Eg, if the repository is instantiated for the Student entity type, the code in the calling method might 
        /// specify q => q.OrderBy(s => s.LastName) for the orderBy parameter.</param>
        /// <param name="includeProperties">parsing the comma-delimited list</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetByIDAsync(object id);

        void Insert(TEntity entity);

        Task DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }

}
