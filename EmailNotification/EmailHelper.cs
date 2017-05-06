using EmailNotification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace EmailNotification
{
    class EmailHelper
    {
        public void SendMail(EmailModel model)
        {
            try
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["FromEmail"],"LeaMaPortal");
                MailMessage mail = new MailMessage();
                List<string> toid = model.ToList.Split(',').ToList();
                foreach (var to in toid)
                {
                    mail.To.Add(to);
                }
                List<string> ccid = model.CCList.Split(',').ToList();
                foreach (var cc in ccid)
                {
                    mail.CC.Add(cc);
                }
                List<string> bccid = model.BCCList.Split(',').ToList();
                foreach (var bcc in bccid)
                {
                    mail.Bcc.Add(bcc);
                }
                mail.Subject = model.Subject;
                mail.Body = model.Body;
                SmtpClient smtp = new SmtpClient();
                var userName = ConfigurationManager.AppSettings["FromEmail"];
                var password = ConfigurationManager.AppSettings["EmailPassword"];
                var basicCredential = new NetworkCredential(userName, password);
                smtp.Host = ConfigurationManager.AppSettings["SMTPHost"];
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = basicCredential;
                mail.From = from;
                mail.Sender = from;
                Console.WriteLine("Sending email...");
                smtp.Send(mail);
                mail.Dispose();
                Console.WriteLine("Mail sent");
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
