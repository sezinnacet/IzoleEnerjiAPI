using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete.ViewModels;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal productDal;
        public ProductManager(IProductDal productDal)
        {
            this.productDal = productDal;
        }
        public List<CategoryProductViewModel> GetAllProductsList()
        {
            return productDal.GetAllProductsList();
        }
    }
}
