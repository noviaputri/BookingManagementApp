using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines Room class and the annotation table
[Table("tb_m_rooms")]
public class Room : BaseClass
{
    // Defines properties for Room class and the annotation column
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }
    [Column("floor")]
    public int Floor { get; set; }
    [Column("capacity")]
    public int Capacity { get; set; }
}
