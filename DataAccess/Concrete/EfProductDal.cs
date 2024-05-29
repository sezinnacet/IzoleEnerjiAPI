using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete.DBModels;
using Entities.Concrete.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EfProductDal : EfEntityRepositoryBase<Product, IzoleEnerjiDbContext>, IProductDal
    {
        public List<CategoryProductViewModel> GetAllProductsList()
        {
            using var context = new IzoleEnerjiDbContext();
            return context.Categories
                         .Include(x => x.Products)
                         .Select(x => new CategoryProductViewModel
                         {
                             CategoryName = x.CategoryName,
                             Products = x.Products.Select(product => new ProductViewModel
                             {
                                 EntityName = product.EntityName,
                                 Price = product.Price,
                                 PriceChangeDate = product.PriceChangeDate,
                                 RValue = product.RValue,
                                 Unit = product.Unit
                             }).ToList()
                         }).ToList();

        }
    }
}
