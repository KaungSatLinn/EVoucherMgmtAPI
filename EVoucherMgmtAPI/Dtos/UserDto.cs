namespace EVoucherMgmtAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int BuyLimit { get; set; }
        public int GiftLimit { get; set; }
    }
}
