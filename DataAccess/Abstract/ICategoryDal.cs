using Core.DataAccess;
using Entities.Concrete.DBModels;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
    }
}
