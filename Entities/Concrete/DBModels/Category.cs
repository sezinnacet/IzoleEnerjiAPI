using Entities.Abstract;

namespace Entities.Concrete.DBModels
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

}
