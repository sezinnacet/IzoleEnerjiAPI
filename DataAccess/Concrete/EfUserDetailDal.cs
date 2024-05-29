using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete.DBModels;
using Entities.Concrete.ViewModels;
using Entities.Enums;

namespace DataAccess.Concrete
{
    public class EfUserDetailDal : EfEntityRepositoryBase<UserDetail, IzoleEnerjiDbContext>, IUserDetailDal
    {
        public UserViewModel? GetUserDetail(User user)
        {
            UserViewModel? result = null;
            if (user != null)
            {
                var userDetail = user.UserDetails.FirstOrDefault(ud => ud.Status == Status.Active);

                PremiumModeViewModel premiumMode = null;
                if (userDetail != null)
                {
                    bool isPremium = IsAvailable(userDetail);
                    if (isPremium)
                    {
                        premiumMode = new PremiumModeViewModel
                        {
                            PremiumModel = Enum.GetName(typeof(Status), userDetail.PremiumMode.PremiumModel),
                            PremiumName = userDetail.PremiumMode.Premium.PremiumName
                        };
                    }

                    result = new UserViewModel
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        UserDetails = new UserDetailViewModel()
                        {
                            IsPremium = userDetail.IsPremium,
                            PremiumEndDate = userDetail.PremiumEndDate,
                            PremiumMode = premiumMode
                        }
                    };
                }

            }
            return result;

        }

        public bool CheckPremium(UserDetail userDetails)
        {
            using var context = new IzoleEnerjiDbContext();

            bool result = IsAvailable(userDetails);
            if (!result && userDetails.IsPremium)
            {
                userDetails.Status = Status.Passive;
                context.UserDetails.Update(userDetails);
                context.SaveChanges();

                userDetails.Id = 0;
                userDetails.Status = Status.Active;
                userDetails.PremiumEndDate = null;
                userDetails.IsPremium = false;
                userDetails.PremiumMode = null;
                context.UserDetails.Add(userDetails);

                context.SaveChanges();
            }

            return result;
        }

        public bool IsAvailable(UserDetail userDetail)
        {
            if (userDetail.IsPremium)
            {
                if (userDetail.PremiumEndDate <= DateTime.Today)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else return false;
        }
    }
}
