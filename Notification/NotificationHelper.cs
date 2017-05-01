using Notification.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class NotificationHelper
    {
        public void sendMail(EmailModel model)
        {
            MailAddress from = new MailAddress(model.from);

            MailMessage mail = new MailMessage();
            foreach(var to in model.toList)
            {
                mail.To.Add(to);
            }
            foreach (var cc in model.ccList)
            {
                mail.CC.Add(cc);
            }
            mail.Subject = model.sub;
            mail.Body = model.body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["SMTPHost"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            smtp.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["FromEmail"], 
                ConfigurationManager.AppSettings["EmailPassword"]);
            smtp.EnableSsl = true;
            Console.WriteLine("Sending email...");
            smtp.Send(mail);
            Console.WriteLine("Mail sent");
        }
    }
}
