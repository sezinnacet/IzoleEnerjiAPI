using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete.DBModels;

namespace DataAccess.Concrete
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, IzoleEnerjiDbContext>, ICategoryDal
    {
    }
}
