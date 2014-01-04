using DataObjects.Interfaces;

namespace DataObjects.EntityFramework.Implementation
{
    public class EntityUserPermission : EntityDao<UserPermission>, IUserPermission
    {
        public EntityUserPermission(LMateEntities context)
            : base(context)
        {
        }
    }
}
