using EVoucherMgmtAPI.Models;

namespace EVoucherMgmtAPI.Dtos
{
    public class GetEVoucherDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public int BuyTypeLimitId { get; set; }
        public bool InActive { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; } = new PaymentMethod();
        public virtual BuyTypeLimit BuyTypeLimit { get; set; } = new BuyTypeLimit();
    }
}
