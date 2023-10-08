using API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handlers;

// Declares public class named TokenHandler that inherits from ITokenHandler.
public class TokenHandler : ITokenHandler
{
    // Declares a private field of type IConfiguration.
    private readonly IConfiguration _configuration;

    // Declares a public constructor that takes an IConfiguration parameter.
    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Declares a public method named Generate that takes an IEnumerable of Claim objects and returns a string.
    public string Generate(IEnumerable<Claim> claims)
    {
        // Creates a new SymmetricSecurityKey object with the secret key specified in the configuration file.
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:SecretKey"]));
        // Creates a new SigningCredentials object with the secret key and the HmacSha256 algorithm.
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        // Creates a new JwtSecurityToken object with the issuer, audience, claims, expiration time, signing credentials specified in the configuration file.
        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"],
            audience: _configuration["JWTService:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingCredentials);
        // Creates an encoded JWT token from the tokenOptions object using the JwtSecurityTokenHandler class.
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        // Returns the encoded JWT token.
        return encodedToken;
    }
}
