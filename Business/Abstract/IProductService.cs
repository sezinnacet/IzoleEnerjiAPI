using Entities.Concrete.ViewModels;

namespace Business.Abstract
{
    public interface IProductService
    {
        public List<CategoryProductViewModel> GetAllProductsList();
    }
}
