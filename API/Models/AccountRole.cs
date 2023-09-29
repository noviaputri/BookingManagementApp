using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines AccountRole class and the annotation table
[Table("tb_m_account_roles")]
public class AccountRole : BaseEntity
{
    // Defines properties for AccountRole class and the annotation column
    [Column("account_guid")]
    public Guid AccountGuid { get; set; }
    [Column("role_guid")]
    public Guid RoleGuid { get; set; }

    // Cardinality
    public Role? Role { get; set; }
    public Account? Account { get; set; }
}
