using Entities.Abstract;
using Entities.Enums;
using System.Text.Json.Serialization;

namespace Entities.Concrete.DBModels
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime InsertDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
