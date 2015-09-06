using System;
using System.Net.Mail;
using System.Text;

namespace BitbucketBackup
{
    class EmailLogger : ILogger
    {
        private readonly MailMessage message = new MailMessage();
        private readonly StringBuilder sb = new StringBuilder();

        public EmailLogger(string emailAddress)
        {
            message.To.Add(emailAddress);
            message.Subject = String.Format(Resources.EmailSubject, DateTime.Now.ToString("dd MMM HH:mm:ss"));
        }

        public void WriteLine()
        {
            WriteLine(Environment.NewLine);
        }

        public void WriteLine(string msg, params object[] args)
        {
            sb.AppendFormat(msg, args);
            sb.AppendLine();
        }

        public void Flush()
        {
            using (var client = new SmtpClient())
            {
                message.Body = sb.ToString();
                client.Send(message);
            }
        }
    }
}