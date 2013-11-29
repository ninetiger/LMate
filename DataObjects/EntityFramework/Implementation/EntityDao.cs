using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// ref: http://www.asp.net/mvc/tutorials/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public class EntityDao<TEntity> where TEntity : class
    {
        internal LMateEntities Context;
        internal DbSet<TEntity> DbSet;

        public EntityDao(LMateEntities context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        /// <param name="filter">The code Expression&lt;Func&lt;TEntity, bool>> filter means the caller will provide a lambda expression based on the TEntity type, and this expression will return a Boolean value. 
        /// Eg, if the repository is instantiated for the Student entity type, the code in the calling method might specify student => student.LastName == "Smith" for the filter parameter.</param>
        /// <param name="orderBy">The code Func&lt;IQueryable&lt;TEntity>, IOrderedQueryable&lt;Entity>> orderBy also means the caller will provide a lambda expression. 
        /// But in this case, the input to the expression is an IQueryable object for the TEntity type. 
        /// The expression will return an ordered version of that IQueryable object. 
        /// Eg, if the repository is instantiated for the Student entity type, the code in the calling method might specify q => q.OrderBy(s => s.LastName) for the orderBy parameter.</param>
        /// <param name="includeProperties">parsing the comma-delimited list</param>
        /// <returns></returns>
        public async virtual Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async virtual Task<TEntity> GetByIDAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async virtual Task DeleteAsync(object id)
        {
            var entityToDelete = await DbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
    }

}