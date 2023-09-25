using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// Defines Booking class and the annotation table
[Table("tb_tr_bookings")]
public class Booking : BaseClass
{
    // Defines properties for Booking class and the annotation column
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    [Column("end_date")]
    public DateTime EndDate { get; set; }
    [Column("status")]
    public StatusLevel Status { get; set; }
    [Column("remarks", TypeName = "nvarchar(max)")]
    public string Remarks { get; set; }
    [Column("room_guid")]
    public Guid RoomGuid { get; set; }
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }
}
