using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines Education class and the annotation table
[Table("tb_m_educations")]
public class Education : BaseEntity
{
    // Defines properties for Education class and the annotation column
    [Column("major", TypeName = "nvarchar(100)")]
    public string Major { get; set; }
    [Column("degree", TypeName = "nvarchar(100)")]
    public string Degree { get; set; }
    [Column("gpa")]
    public float Gpa { get; set; }
    [Column("university_guid")]
    public Guid UniversityGuid { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
    public University? University { get; set; }
}
