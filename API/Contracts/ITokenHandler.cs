using System.Security.Claims;

namespace API.Contracts;

// Declares a new public interface named ITokenHandler.
public interface ITokenHandler
{
    // Defines a method for generating a token from the specified collection of claims.
    string Generate(IEnumerable<Claim> claims);
}
