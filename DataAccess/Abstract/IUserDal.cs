using Core.DataAccess;
using Entities.Concrete.DBModels;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        public User? GetUserByEmail(string email);
    }
}
