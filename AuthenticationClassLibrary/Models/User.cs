using System.Text.Json.Serialization;

namespace AuthenticationClassLibrary.Models
{
    public class User
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        public DateTime? Last_Password_Change { get; set; }
        public bool? isActive { get; set; }
        public int? RoleID { get; set; }
    }
}
