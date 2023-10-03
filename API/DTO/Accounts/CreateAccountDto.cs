using API.Models;

namespace API.DTO.Accounts;

// Declares a new public class named CreateAccountDto.
public class CreateAccountDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Password { get; set; } // Declares a public property named Password of type string.
    public int Otp { get; set; } // Declares a public property named Otp of type int.
    public bool IsUsed { get; set; } // Declares a public property named IsUsed of type boolean.
    public DateTime ExpiredTime { get; set; } // Declares a public property named ExpiredTime of type DateTime.

    // Declares a public static implicit conversion operator that takes a CreateAccountDto parameter and returns a Account object.
    public static implicit operator Account(CreateAccountDto createAccountDto)
    {
        // Returns a new Account object with the properties of the createAccountDto parameter.
        return new Account
        {
            Guid = createAccountDto.Guid,
            Password = createAccountDto.Password,
            Otp = createAccountDto.Otp,
            IsUsed = createAccountDto.IsUsed,
            ExpiredTime = createAccountDto.ExpiredTime,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
