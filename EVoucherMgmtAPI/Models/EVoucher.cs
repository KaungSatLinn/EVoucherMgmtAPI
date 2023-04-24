using System.ComponentModel.DataAnnotations.Schema;

namespace EVoucherMgmtAPI.Models
{
    [Table("e_voucher")]
    public class EVoucher : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }
        public decimal Amount { get; set; }
        [Column("payment_method_id")]
        public int PaymentMethodId { get; set; }
        [Column("buy_type_limit_id")]
        public int BuyTypeLimitId { get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; } = false;
        [Column("in_active")]
        public bool InActive { get; set; } = true;

        public virtual PaymentMethod PaymentMethod { get; set; } = new PaymentMethod();
        public virtual BuyTypeLimit BuyTypeLimit { get; set; } = new BuyTypeLimit();
    }
}
