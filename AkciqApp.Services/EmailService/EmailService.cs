namespace AkciqApp.Services.EmailService
{
    using MailKit.Security;
    using MimeKit;
    using MimeKit.Text;

    public class EmailService : IEmailService
    {

        private readonly string user = "";
        private readonly string pass = "";

        public EmailService()
        {
        }

        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(from);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(user, pass);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        public void SendForgottenPass(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("register-service");
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(user, pass);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
