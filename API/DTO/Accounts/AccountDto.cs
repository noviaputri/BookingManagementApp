using API.Models;

namespace API.DTO.Accounts;

// Declares a new public class named AccountDto.
public class AccountDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public int Otp { get; set; } // Declares a public property named Otp of type int.
    public bool IsUsed { get; set; } // Declares a public property named IsUsed of type boolean.
    public DateTime ExpiredTime { get; set; } // Declares a public property named ExpiredTime of type DateTime.

    // Declares a public static explicit conversion operator that takes an Account parameter and returns an AccountDto object.
    public static explicit operator AccountDto(Account account)
    {
        // Returns a new AccountDto object with the properties of the account parameter
        return new AccountDto
        {
            Guid = account.Guid,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime,
        };
    }
}
