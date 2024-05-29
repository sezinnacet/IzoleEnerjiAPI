using Entities.Abstract;
using Entities.Enums;

namespace Entities.Concrete.DBModels
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string EntityName { get; set; }
        public Units Unit { get; set; }
        public decimal Price { get; set; }
        public decimal RValue { get; set; }
        public DateTime PriceChangeDate { get; set; }

        public virtual Category Category { get; set; }
    }

}
