namespace API.Contracts;

// Defines interface class for EmailHandler
public interface IEmailHandler
{
    // Declares method Send
    void Send(string subject , string body, string toEmail);
}
