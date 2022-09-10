using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Library.StockMarketTracker.Models
{
    [Table("Wallets")]
    public class Wallet : BaseModel
    {
        [JsonProperty("walletId")]
        [PrimaryKey("WalletId", false)]
        public int WalletId { get; set; }

        [JsonProperty("balance")]
        [Required]
        [Column("Balance")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Balance must be higher than zero.")]
        public int Balance { get; set; }

        [Column("UserId")]
        public int? UserId { get; set; }
    }
}
