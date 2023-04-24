using System.ComponentModel.DataAnnotations.Schema;

namespace EVoucherMgmtAPI.Models
{
    [Table("promo_code")]
    public class PromoCode : BaseModel
    {
        public string Code { get; set; }
        [Column("qr_image")]
        public byte QRImage { get; set; }
    }
}
