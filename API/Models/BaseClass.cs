using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines abstract class
public abstract class BaseClass
{
    // Defines properties for BaseClass and the annotations
    [Key, Column("guid")]
    public Guid Guid { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }
}
