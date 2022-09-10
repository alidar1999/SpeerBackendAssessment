using System.ComponentModel.DataAnnotations;
using Common.Data.User;
using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Common.Models
{
    [Table("Users")]
    public class User : BaseModel, IUser
    {
        [JsonProperty("id")]
        [PrimaryKey("UserId", false)]
        public int Id { get; set; }

        [JsonProperty("username")]
        [Required]
        [Column("Username")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid Username. Only alphanumeric characters allowed.")]
        public string Username { get; set; } = "";

        [JsonProperty("password")]
        [Required]
        [Column("Password")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,15}$",
            ErrorMessage = "Invalid Password. " +
            "Password should be 8 to 15 characters. " +
            "Password should have at least 1 digit. " +
            "Password should have at least 1 special character. " +
            "Password should have at least 1 upper case alphabet. " +
            "Password should have at least 1 lower case alphabet. ")]
        public string? Password { get; set; } = "";
    }
}
