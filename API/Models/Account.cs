namespace API.Models;

public class Account : BaseClass
{
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }
}
