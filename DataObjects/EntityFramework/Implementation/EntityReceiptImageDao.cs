using BusinessObjects;
using DataObjects.EntityFramework.ModelMapper;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityReceiptImageDao : IReceiptImageDao
    {
        public async Task SaveAsync(ReceiptImage bo)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                ReceiptImages entity;
                if (bo.Id == 0)
                {
                    entity = Mapper.Map(bo);
                    context.ReceiptImages.Add(entity);
                }
                else
                {
                    entity = await context.ReceiptImages.FindAsync(bo.Id);
                    if (entity != null)
                    {
                        entity.Id = bo.Id;
                        entity.ImageData = bo.ImageData;
                        entity.ImageMimeType = bo.ImageMimeType;
                        entity.Description = bo.Description;
                        entity.Date = bo.Date;
                        entity.IsActive = bo.IsActive;
                        Datd
                        entity.AspNetUsers.Add();
                    }
                }

                try
                {
                    await context.SaveChangesAsync();
                    //if (entity != null)
                    //{
                    //    bo.Version = Convert.ToBase64String(entity.Version); //todo need to test if return version is correct
                    //}
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public async Task DeleteAsync(ReceiptImage bo)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = await context.ReceiptImages.SingleOrDefaultAsync(c => c.Id == bo.Id);
                if (entity != null)
                {
                    context.ReceiptImages.Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        public IQueryable<ReceiptImage> GetAllByUserId(string userId, string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var userList = context.ReceiptImages.Select(x => x.AspNetUsers);
                // //var entities = from image in context.ReceiptImages
                // //               join userImage in context.im
                // var entities = context.ReceiptImages
                //.Where(x => x.AspNetUsers.ToList().Where(u => u.Id == userid))
                //     //.OrderBy(sortExpression);
                //     .Select(Mapper.Map);


                // return entities.AsQueryable();
                return null;
            }
        }
    }
}
