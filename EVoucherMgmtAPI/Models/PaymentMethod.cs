using System.ComponentModel.DataAnnotations.Schema;

namespace EVoucherMgmtAPI.Models
{
    [Table("payment_method")]
    public class PaymentMethod : BaseModel
    {
        public string Method { get; set; }
        [Column("discount_percent")]
        public int DiscountPercent { get; set; }

    }
}
