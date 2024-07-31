using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace sem2.riverStations.urbanec.ait4.Services
{
    public class EmailSender:IEmailSender
    {
        public EmailSender()
        {
        }


        public async Task SendEmailAsync(string to, string subject, string body)
        {
            foreach (var recipient in to.Split(';'))
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("vosait@outlook.com");
                    mail.To.Add(recipient);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    using (var smtp = new SmtpClient("smtp-mail.outlook.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("vosait@outlook.com", "Internet123");
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
        }
        public async Task SendCriticalAlertEmail(string to, string subject, string body)
        {
            SendEmailAsync(to, subject, body);
           
        }
    }
}
