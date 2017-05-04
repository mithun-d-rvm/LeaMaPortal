using EmailNotification.DBContext;
using EmailNotification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            const string MAIL_STATUS = "Sent";
            try
            {
                LeamaEntities db = new LeamaEntities();
                EmailHelper emailHelper = new EmailHelper();
                var data = db.email_output.Where(w => string.IsNullOrEmpty(w.Mailstatus))
                    .Select(s => new EmailModel
                    {
                        Id = s.id,
                        ToList = s.toid,
                        CCList = s.cc,
                        Subject = s.Subject,
                        Body = s.body,
                        BCCList=s.bcc
                    }).ToList();
                foreach (var obj in data)
                {
                    emailHelper.SendMail(obj);
                    email_output model = db.email_output.FirstOrDefault(f => f.id == obj.Id);
                    model.Mailstatus = MAIL_STATUS;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
    }
}
