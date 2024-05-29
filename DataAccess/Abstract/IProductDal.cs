using Core.DataAccess;
using Entities.Concrete.DBModels;
using Entities.Concrete.ViewModels;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        public List<CategoryProductViewModel> GetAllProductsList();
    }
}
