using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete.DBModels;

namespace DataAccess.Concrete
{
    public class EfPremiumDal : EfEntityRepositoryBase<Premium, IzoleEnerjiDbContext>, IPremiumDal
    {
    }
}
