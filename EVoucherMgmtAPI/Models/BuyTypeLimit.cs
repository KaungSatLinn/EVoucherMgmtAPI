using System.ComponentModel.DataAnnotations.Schema;

namespace EVoucherMgmtAPI.Models
{
    [Table("buy_type_limit")]
    public class BuyTypeLimit : BaseModel
    {
        [Column("self_limit")]
        public int SelfLimit { get; set; }
        [Column("gift_limit")]
        public int GiftLimit { get; set; }
    }
}
