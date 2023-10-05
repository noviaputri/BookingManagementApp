namespace API.Contracts;

// Defines interface class for EmailHandler
public interface IEmailHandler
{
    void Send(string subject , string body, string toEmail);
}
