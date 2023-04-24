using System.ComponentModel.DataAnnotations.Schema;

namespace EVoucherMgmtAPI.Models
{
    public class User : BaseModel
    {
        [Column("user_name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
