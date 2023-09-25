using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_account_roles")]
public class AccountRole : BaseClass
{
    [Column("account_guid")]
    public Guid AccountGuid { get; set; }
    [Column("role_guid")]
    public Guid RoleGuid { get; set; }
}
