using Entities.Concrete.RequestModels;
using Entities.Concrete.ViewModels;

namespace Business.Abstract
{
    public interface IUserService
    {
        UserViewModel GetUserDetail(string email);
        UserViewModel AddUser(AddUserRequestModel user);
        UserViewModel UpgradeToPremium(UpgradePremiumRequestModel model);
        bool isSignedUp(UserDetailRequestModel model, bool isGoogle = false);
    }
}
