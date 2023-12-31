﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines Role class and the annotation table
[Table("tb_m_roles")]
public class Role : BaseEntity
{
    // Defines property for Role class and the annotation column
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    // Cardinality
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
