using Entities.Abstract;
using Entities.Enums;
using System.Text.Json.Serialization;

namespace Entities.Concrete.DBModels
{
    public class UserDetail : IEntity
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsPremium { get; set; }
        public DateTime? PremiumEndDate { get; set; }
        public int? PremiumModeId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual PremiumMode? PremiumMode { get; set; }
    }

}
