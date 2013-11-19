using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.EntityFramework.ModelMapper;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityAccountTypeDao : IAccountTypeDao
    {
        public AccountType GetAccountType(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = context.AccountTypes.FirstOrDefault(c => c.Id == id);
                return Mapper.Map(query);
            }
        }
        public async Task<AccountType> GetAccountTypeAsync(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = await context.AccountTypes.FirstOrDefaultAsync(c => c.Id == id);
                return Mapper.Map(query);
            }
        }

        public IQueryable<AccountType> GetAccountTypesByUser(string userId, string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.AccountTypes.AsQueryable()
                                .OrderBy(sortExpression)
                                .Where(c => c.User_Id == userId || c.User_Id == null);

                var list = new List<AccountType>();
                foreach (var receipt in entities)
                    list.Add(Mapper.Map(receipt));

                return list.AsQueryable();
            }
        }

        public async Task<IQueryable<AccountType>> GetAccountTypesByUserAsync(string userId, string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.AccountTypes.AsQueryable()
                                .OrderBy(sortExpression)
                                .Where(c => c.User_Id == userId || c.User_Id == null);

                var list = await entities.Select(entity => Mapper.Map(entity)).ToListAsync();

                return list.AsQueryable();
            }
        }
    }
}
