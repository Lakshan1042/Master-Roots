using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;
    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var smtpHost = _config["EmailSettings:SMTPHost"];
        var smtpPort = int.Parse(_config["EmailSettings:SMTPPort"]);
        var smtpUser = _config["EmailSettings:SMTPUser"];
        var smtpPass = _config["EmailSettings:SMTPPass"];
        var enableSSL = bool.Parse(_config["EmailSettings:EnableSSL"]);

        var client = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = enableSSL
        };

        return client.SendMailAsync(
            new MailMessage(smtpUser, email, subject, htmlMessage) { IsBodyHtml = true }
        );
    }
}
