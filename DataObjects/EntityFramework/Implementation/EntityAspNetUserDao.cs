using System;
using BusinessObjects;
using DataObjects.EntityFramework.ModelMapper;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityAspNetUserDao : IAspNetUserDao
    {
        public async Task<AspNetUsers> GetByIdAsync(string id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = await context.AspNetUsers.FindAsync(id);

                return query;
            }
        }

        public Task SaveAsync(AspNetUsers bo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AspNetUsers bo)
        {
            throw new NotImplementedException();
        }

        public Task<AspNetUsers> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AspNetUsers> GetAll(string sortExpression = "Id ASC")
        {
            throw new NotImplementedException();
        }
    }
}
