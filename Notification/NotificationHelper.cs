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
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);

            MailMessage mail = new MailMessage();
            List<string> toid = model.ToList.Split(',').ToList();
            foreach(var to in toid)
            {
                mail.To.Add(to);
            }
            List<string> ccid = model.CCList.Split(',').ToList();
            foreach (var cc in ccid)
            {
                mail.CC.Add(cc);
            }
            mail.Subject = model.Subject;
            mail.Body = model.Body;

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
