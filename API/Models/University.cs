using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines University class and the annotation table
[Table("tb_m_universities")]
public class University : BaseClass
{
    // Defines properties for University class and the annotation column
    [Column("code", TypeName = "nvarchar(50)")]
    public string Code { get; set; }
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }
}
