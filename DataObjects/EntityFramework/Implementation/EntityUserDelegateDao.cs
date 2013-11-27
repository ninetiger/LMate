namespace DataObjects.EntityFramework.Implementation
{
    public class EntityUserDelegateDao : EntityDao<UserDelegate>, IUserDelegateDao
    {
        public EntityUserDelegateDao(LMateEntities context)
            : base(context)
        {
        }
    }
}
