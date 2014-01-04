namespace DataObjects.EntityFramework.Implementation
{
    public class EntityAspNetRoleDao : EntityDao<AspNetRole>, IAspNetRoleDao
    {
        public EntityAspNetRoleDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
