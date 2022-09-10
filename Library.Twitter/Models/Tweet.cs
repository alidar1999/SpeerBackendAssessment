using Postgrest.Models;
using Postgrest.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Library.Twitter.Models
{
    [Table("Tweets")]
    public class Tweet : BaseModel
    {
        [JsonProperty("id")]
        [PrimaryKey("TweetId", false)]
        public int Id { get; set; }

        [JsonProperty("subject")]
        [Required]
        [Column("Subject")]
        public string Subject { get; set; } = "";

        [JsonProperty("description")]
        [Required]
        [Column("Description")]
        public string? Description { get; set; } = "";

        [JsonProperty("createDateTime")]
        [Column("TweetCreateDateTimeUTC", NullValueHandling.Ignore)]
        public DateTime? TweetCreateDateTimeUTC { get; set; } = DateTime.UtcNow;

        [JsonProperty("modifiedDateTime")]
        [Column("TweetModifiedDateTimeUTC", NullValueHandling.Ignore)]
        public DateTime? TweetModifiedDateTimeUTC { get; set; } = DateTime.UtcNow;

        [Column("UserId")]
        public int UserId { get; set; }
    }
}
