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
    public class EntityReceiptTypeDao : IReceiptTypeDao
    {
        public ReceiptType GetReceiptType(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = context.ReceiptTypes.FirstOrDefault(c => c.Id == id);
                return Mapper.Map(query);
            }
        }
        public async Task<ReceiptType> GetReceiptTypeAsync(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = await context.ReceiptTypes.FirstOrDefaultAsync(c => c.Id == id);
                return Mapper.Map(query);
            }
        }

        public IQueryable<ReceiptType> GetReceiptTypesByUser(string userId, string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.ReceiptTypes.AsQueryable()
                                .OrderBy(sortExpression)
                                .Where(c => c.User_Id == userId || c.User_Id == null);

                var list = new List<ReceiptType>();
                foreach (var receipt in entities)
                    list.Add(Mapper.Map(receipt));

                return list.AsQueryable();
            }
        }

        public async Task<IQueryable<ReceiptType>> GetReceiptTypesByUserAsync(string userId, string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.ReceiptTypes.AsQueryable()
                                .OrderBy(sortExpression)
                                .Where(c => c.User_Id == userId || c.User_Id == null);

                var list = await entities.Select(entity => Mapper.Map(entity)).ToListAsync();

                return list.AsQueryable();
            }
        }
    }
}
