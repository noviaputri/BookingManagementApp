using API.Models;

namespace API.DTO.Accounts;

// Declares a new public class named ChangePasswordDto.
public class ChangePasswordDto
{
    public string Email { get; set; } // Declares a public property named Email of type string.
    public int Otp { get; set; } // Declares a public property named Otp of type string.
    public string NewPassword { get; set; } // Declares a public property named NewPassword of type string.
    public string ConfirmPassword { get; set; } // Declares a public property named ConfirmPassword of type string.


    // Declares a public static implicit conversion operator that takes a ChangePasswordDto parameter and returns a Account object.
    public static implicit operator Account(ChangePasswordDto changePasswordDto)
    {
        // Returns a new Account object with the properties of the changePasswordDto parameter.
        return new Account
        {
            Password = changePasswordDto.NewPassword,
            Otp = changePasswordDto.Otp,
            IsUsed = true,
            ModifiedDate = DateTime.Now
        };
    }
}
