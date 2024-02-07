namespace DeklarimiPasuris;
using System.Net;
using System.Net.Mail;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("apksnotifier@gmail.com", "ldzp iwxf zvuh imgr")
        };

        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Timeout = 1000;

        return client.SendMailAsync(
            new MailMessage(from: "apksnotifier@gmail.com",
                            to: email,
                            subject,
                            message
                            ));
    }
}
