using System.Linq;
using System.Linq.Dynamic;
using BusinessObjects;
using DataObjects.EntityFramework.ModelMapper;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityReceiptCategoryDao : IReceiptCategoryDao
    {
        public async Task SaveAsync(ReceiptCategory bo)
        {
            //todo wrong need both new/update
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = await context.ReceiptCategories.SingleOrDefaultAsync(c => c.Id == bo.Id);
                if (entity != null)
                {
                    context.ReceiptCategories.Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAsync(ReceiptCategory bo)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = await context.ReceiptCategories.SingleOrDefaultAsync(c => c.Id == bo.Id);
                if (entity != null)
                {
                    context.ReceiptCategories.Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<ReceiptCategory> GetByIdAsync(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = await context.ReceiptCategories.FirstOrDefaultAsync(c => c.Id == id);
                return query == null ? new ReceiptCategory() : Mapper.Map(query);
            }
        }

        public IQueryable<ReceiptCategory> GetAll(string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.ReceiptCategories.OrderBy(sortExpression).Select(Mapper.Map);
                return entities.AsQueryable();
            }
        }

        public IQueryable<ReceiptCategory> GetAllByUserId(string userId, string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.ReceiptCategories
                    .Where(c => c.User_Id == userId)
                    .OrderBy(sortExpression)
                    .Select(Mapper.Map);

                return entities.AsQueryable();
            }
        }
    }
}
