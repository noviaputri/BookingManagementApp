using API.Models;

namespace API.DTO.Accounts;

// Declares a new public class named AccountDto.
public class AccountDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Password { get; set; } // Declares a public property named Password of type string.
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
            Password = account.Password,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }

    // Declares a public static implicit conversion operator that takes a AccountDto parameter and returns a Account object.
    public static implicit operator Account(AccountDto accountDto)
    {
        // Returns a new Account object with the properties of the accountDto parameter.
        return new Account
        {
            Guid = accountDto.Guid,
            Password = accountDto.Password,
            Otp = accountDto.Otp,
            IsUsed = accountDto.IsUsed,
            ExpiredTime = accountDto.ExpiredTime,
            ModifiedDate = DateTime.Now
        };
    }
}
