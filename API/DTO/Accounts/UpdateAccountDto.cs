﻿using API.Models;

namespace API.DTO.Accounts;

// Declares a new public class named UpdateAccountDto.
public class UpdateAccountDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Password { get; set; } // Declares a public property named Password of type string.
    public int Otp { get; set; } // Declares a public property named Otp of type int.
    public bool IsUsed { get; set; } // Declares a public property named IsUsed of type boolean.
    public DateTime ExpiredTime { get; set; } // Declares a public property named ExpiredTime of type DateTime.

    // Declares a public static implicit conversion operator that takes a AccountDto parameter and returns a Account object.
    public static implicit operator Account(UpdateAccountDto accountDto)
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
