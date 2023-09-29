using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines Account class and the annotation table
[Table("tb_m_accounts")]
public class Account : BaseEntity
{
    // Defines properties for Account class and the annotation column
    [Column("password", TypeName = "nvarchar(max)")]
    public string Password { get; set; }
    //public bool IsDeleted { get; set; }
    [Column("otp")]
    public int Otp { get; set; }
    [Column("is_used")]
    public bool IsUsed { get; set; }
    [Column("expired_time")]
    public DateTime ExpiredTime { get; set; }

    // Cardinality
    public ICollection<AccountRole>? AccountRoles { get; set; }
    public Employee? Employee { get; set; }
}
