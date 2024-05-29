using Entities.Enums;

namespace Entities.Concrete.ViewModels
{
    public class CategoryProductViewModel
    {
        public string CategoryName { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }

    public class ProductViewModel
    {
        public string EntityName { get; set; }
        public Units Unit { get; set; }
        public decimal RValue { get; set; }
        public decimal Price { get; set; }
        public DateTime PriceChangeDate { get; set; }
    }
}
