using System.ComponentModel.DataAnnotations.Schema;

namespace EVoucherMgmtAPI.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        [Column("is_delete")]
        public int IsDelete { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column("created_by")]
        public int CreatedBy { get; set; } = 1;
        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }
    }
}
