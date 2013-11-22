using System.Linq;
using System.Linq.Dynamic;
using BusinessObjects;
using DataObjects.EntityFramework.ModelMapper;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityCurrencyDao : ICurrencyDao
    {
        public async Task SaveAsync(Currency bo)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = await context.Currencies.SingleOrDefaultAsync(c => c.Id == bo.Id);
                if (entity != null)
                {
                    context.Currencies.Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAsync(Currency bo)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = await context.Currencies.SingleOrDefaultAsync(c => c.Id == bo.Id);
                if (entity != null)
                {
                    context.Currencies.Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<AspNetUsers> GetByIdAsync(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = await context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
                return query == null ? new Currency() : Mapper.Map(query);
            }
        }

        public IQueryable<Currency> GetAll(string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.Currencies.OrderBy(sortExpression).Select(Mapper.Map);
                return entities.AsQueryable();
            }
        }
    }
}
