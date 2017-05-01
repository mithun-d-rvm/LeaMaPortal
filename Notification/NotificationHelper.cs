using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class NotificationHelper
    {
        public void sendMail()
        {
            Console.WriteLine("Mail To");
            MailAddress to = new MailAddress(Console.ReadLine());

            Console.WriteLine("Mail From");
            MailAddress from = new MailAddress(Console.ReadLine());

            MailMessage mail = new MailMessage(from, to);

            Console.WriteLine("Subject");
            mail.Subject = Console.ReadLine();

            Console.WriteLine("Your Message");
            mail.Body = Console.ReadLine();

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                "username@domain.com", "password");
            smtp.EnableSsl = true;
            Console.WriteLine("Sending email...");
            smtp.Send(mail);
        }
    }
}
