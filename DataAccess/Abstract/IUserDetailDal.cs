using Core.DataAccess;
using Entities.Concrete.DBModels;
using Entities.Concrete.ViewModels;

namespace DataAccess.Abstract
{
    public interface IUserDetailDal : IEntityRepository<UserDetail>
    {
        public UserViewModel GetUserDetail(User user);
        public bool CheckPremium(UserDetail userDetails);
        public bool IsAvailable(UserDetail userDetail);
    }
}
