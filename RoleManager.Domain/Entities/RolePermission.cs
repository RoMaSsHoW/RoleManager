using System.Text.Json.Serialization;

namespace RoleManager.Domain.Entities
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
        public Guid PermissionId { get; set; }
        [JsonIgnore]
        public Permission Permission { get; set; }
    }
}
