namespace API.Utilities.Handlers;

// Declares public class named ExceptionHandler that inherits from the Exception class.
public class ExceptionHandler : Exception
{
    // Declares a public constructor that takes a string parameter and calls the base constructor with the message parameter.
    public ExceptionHandler(string message) : base(message) { }
}
