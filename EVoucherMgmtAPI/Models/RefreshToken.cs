using System.ComponentModel.DataAnnotations.Schema;

namespace EVoucherMgmtAPI.Models
{
    [Table("refresh_token")]
    public class RefreshToken : BaseModel
    {
        [Column("user_id")]
        public int UserId { get; set; }
        public string Token { get; set; }
        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }
    }
}
