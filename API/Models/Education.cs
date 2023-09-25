namespace API.Models;

public class Education : BaseClass
{
    public string Major { get; set; }
    public string Degree { get; set; }
    public double Gpa { get; set; }
    public Guid UniversityGuid { get; set; }
}
