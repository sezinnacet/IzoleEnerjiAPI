using Entities.Abstract;

namespace Entities.Concrete.DBModels
{
    public class Premium : IEntity
    {
        public int Id { get; set; }
        public string PremiumName { get; set; }

    }

}
