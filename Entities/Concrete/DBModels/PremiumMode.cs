using Entities.Abstract;
using Entities.Enums;
using System.Text.Json.Serialization;

namespace Entities.Concrete.DBModels
{
    public class PremiumMode : IEntity
    {
        public int Id { get; set; }
        public int PremiumId { get; set; }
        public PremiumModels PremiumModel { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual Premium Premium { get; set; }

    }

}
