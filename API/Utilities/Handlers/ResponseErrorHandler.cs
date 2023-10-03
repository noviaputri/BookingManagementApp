namespace API.Utilities.Handlers;

// Declares public class named ResponseErrorHandler.
public class ResponseErrorHandler
{
    // Declares all public property for ResponseErrorHandler class.
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public string? Error { get; set; }
}
