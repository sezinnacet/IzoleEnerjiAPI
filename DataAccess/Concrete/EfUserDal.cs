using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete.DBModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, IzoleEnerjiDbContext>, IUserDal
    {
        public User? GetUserByEmail(string email)
        {
            using var context = new IzoleEnerjiDbContext();
            return context.Users
                                .Include(u => u.UserDetails)
                                .ThenInclude(ud => ud.PremiumMode)
                                .ThenInclude(pm => pm.Premium)
                                .FirstOrDefault(u => u.Email == email);
        }
    }
}
