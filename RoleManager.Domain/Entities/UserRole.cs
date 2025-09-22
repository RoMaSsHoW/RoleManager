using System.Text.Json.Serialization;

namespace RoleManager.Domain.Entities
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
        public Guid RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; } = null!;
    }
}
