using System.Net;
using System.Net.Mail;

namespace SiteOrganiser.DataAccess.Helpers
{
    public static class EmailHelper
    {
        public static void Send(string to, string subject, string body)
        {
            var smtp = new SmtpClient();
            var mail = new MailMessage(((NetworkCredential)smtp.Credentials).UserName, to, subject, body) { IsBodyHtml = true };
            smtp.Send(mail);
        }

        public static void Send(string from, string parola, string to, string subject, string body)
        {
            using (var mailgonder = new MailMessage())
            {
                mailgonder.To.Add(to); //mail göndermek istediğiniz herhangi bir hesap
                mailgonder.From = new MailAddress(@from);
                mailgonder.Subject = subject;
                mailgonder.IsBodyHtml = true; // html içerik gönderiyorsanız true düz metin ise false olacaktır.
                mailgonder.Body = body;

                var client = new SmtpClient
                {
                    Credentials = new NetworkCredential(@from, parola),
                    Port = 25,
                    Host = "mail.benimyurdum.com",
                    EnableSsl = false
                };

                client.Send(mailgonder);
            }
        }
    }
}
