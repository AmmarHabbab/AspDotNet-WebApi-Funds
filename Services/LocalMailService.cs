namespace WebApi1.Services;

public class LocalMailService
{
    private string _mailTo = "admin@mycompany.com";
    private string _mailFrom = "noreply@mycompany.com";

public void Send(string subject, string message)
{
    // send mail - output to console window
    Console.WriteLine($"Mail From {_mailFrom} to {_mailTo}, " + $"with {nameof(LocalMailService)}.");
    Console.WriteLine($"Subject: {subject}");
    Console.WriteLine($"Message: {message}");
}
    
}
