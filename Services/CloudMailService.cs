namespace WebApi1.Services;

public class CloudMailService : IMailService
{
    private string _mailTo = string.Empty;
    private string _mailFrom = string.Empty;

    public CloudMailService(IConfiguration configuration)
    {
        _mailTo = configuration["mailSettings:mailToAddress"];
        _mailFrom = configuration["mailSettings:mailFromAddress"];
    }
public void Send(string subject, string message)
{
    // send mail - output to console window
    Console.WriteLine($"Mail From {_mailFrom} to {_mailTo}, " + $"with {nameof(CloudMailService)}.");
    Console.WriteLine($"Subject: {subject}");
    Console.WriteLine($"Message: {subject}");
}
    
}
