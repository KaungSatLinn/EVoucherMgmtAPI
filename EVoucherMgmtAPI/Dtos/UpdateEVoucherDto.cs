namespace EVoucherMgmtAPI.Dtos
{
    public class UpdateEVoucherDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public int BuyTypeLimitId { get; set; }
        public bool InActive { get; set; }
    }
}
