using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete.DBModels;
using Entities.Concrete.RequestModels;
using Entities.Concrete.ViewModels;
using Entities.Enums;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDetailDal userDetailDal;
        private readonly IUserDal userDal;
        private readonly IPremiumModeDal premiumModeDal;
        private readonly IMapper mapper;

        public UserManager(IUserDetailDal userDetailDal, IUserDal userDal, IPremiumModeDal premiumModeDal, IMapper mapper)
        {
            this.userDetailDal = userDetailDal;
            this.userDal = userDal;
            this.premiumModeDal = premiumModeDal;
            this.mapper = mapper;
        }

        public UserViewModel AddUser(AddUserRequestModel userReqModel)
        {
            if (userDal.Any(x => x.Status == Status.Active && x.Email.ToLower() == userReqModel.Email.ToLower()))
            {
                throw new Exception("Bu mail adresi kullanımda!");
            }
            User user = mapper.Map<User>(userReqModel);
            user = userDal.Add(user);

            AddUserDetail(user);
            user = userDal.GetUserByEmail(user.Email);
            return userDetailDal.GetUserDetail(user);
        }
        private UserDetail AddUserDetail(User user)
        {
            return userDetailDal.Add(new UserDetail
            {
                Id = 0,
                Status = Status.Active,
                UserId = user.Id,
                LastLoginDate = DateTime.Now,
                IsPremium = false,
                PremiumEndDate = null,
                PremiumModeId = null,
            });
        }
        public UserViewModel GetUserDetail(string email)
        {
            var user = userDal.GetUserByEmail(email);
            return userDetailDal.GetUserDetail(user);
        }

        public bool isSignedUp(UserDetailRequestModel model, bool isGoogle = false)
        {
            var user = userDal.GetUserByEmail(model.Email);
            if (user == null || user.Password != model.Password)
            {
                if (isGoogle) return false;
                throw new Exception("Email veya şifre hatalı!");
            }
            var userDetail = user.UserDetails.FirstOrDefault(ud => ud.Status == Status.Active);
            if (userDetail != null)
            {
                userDetailDal.CheckPremium(userDetail);
                return true;
            }
            else
            {
                AddUserDetail(user);
                return true;
            }
        }

        public UserViewModel UpgradeToPremium(UpgradePremiumRequestModel model)
        {

            var user = userDal.GetUserByEmail(model.Email);

            var userDetail = user?.UserDetails.FirstOrDefault(ud => ud.Status == Status.Active);

            if (userDetail != null)
            {
                if (userDetailDal.CheckPremium(userDetail))
                {
                    throw new Exception("Zaten premium üyeliğiniz mevcut!");
                }
                userDetail.Status = Status.Passive;
                userDetailDal.Update(userDetail);

                switch (model.Premium)
                {
                    case PremiumModels.Daily:
                        userDetail.PremiumEndDate = DateTime.Today.AddDays(2);
                        break;
                    case PremiumModels.Monthly:
                        userDetail.PremiumEndDate = DateTime.Today.AddMonths(1).AddDays(1);
                        break;
                    case PremiumModels.Yearly:
                        userDetail.PremiumEndDate = DateTime.Today.AddYears(1).AddDays(1);
                        break;
                    default:
                        throw new Exception("Premium modu bulunamadı!");
                }

                userDetail.Id = 0;
                userDetail.Status = Status.Active;
                userDetail.IsPremium = true;
                userDetail.PremiumModeId = premiumModeDal.GetList().FirstOrDefault().Id;
                userDetail.LastLoginDate = DateTime.Now;

                userDetailDal.Add(userDetail);
                return GetUserDetail(user.Email);

            }
            throw new Exception("Kullanıcı bilgilerinde hata!");
        }
    }
}
