namespace DataObjects.EntityFramework.Implementation
{
    public class EntityAspNetUserDao : EntityDao<AspNetUser>, IAspNetUserDao
    {
        public EntityAspNetUserDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
