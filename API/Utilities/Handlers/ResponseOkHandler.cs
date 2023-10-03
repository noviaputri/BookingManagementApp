using System.Net;

namespace API.Utilities.Handlers;

// Declares public class named ResponseOkHandler with a generic type parameter TEntity.
public class ResponseOkHandler<TEntity>
{
    // Declares all public property for ResponseOkHandler class.
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public TEntity? Data { get; set; }

    // Declares a public constructor that takes a nullable TEntity parameter.
    public ResponseOkHandler(TEntity? data)
    {
        // Assigns the value of every ResponseOkHandler property.
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = "Success to Retrieve Data";
        Data = data;
    }

    // Declares a public constructor that takes a string parameter.
    public ResponseOkHandler(string message)
    {
        // Assigns the value of every ResponseOkHandler property.
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = message;
    }
}
