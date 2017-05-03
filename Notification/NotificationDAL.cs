using LeaMaPortal.DBContext;
using System.Threading.Tasks;
using System.Data.Entity;
using System;
using Notification.Model;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using LeaMaPortal.Helpers;

namespace Notification
{
    public class NotificationDAL
    {
        private LeamaEntities db = new LeamaEntities();
        internal async Task<bool> dailyNotification()
        {
            try
            {
                NotificationHelper notify = new NotificationHelper();
                var data = await db.email_output.Where(w => string.IsNullOrEmpty(w.Mailstatus))
                            .Select(s => new EmailModel
                            {
                                Id = s.id,
                                ToList = s.toid,
                                CCList = s.cc,
                                Subject = s.Subject,
                                Body = s.body
                            }).ToListAsync();
                Type myType = data.GetType();

                foreach (var obj in data)
                {                    
                    notify.sendMail(obj);
                    email_output model = await db.email_output.FirstOrDefaultAsync(f => f.id == obj.Id);
                    model.Mailstatus = StaticHelper.MAIL_SENT;
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        internal async Task<bool> weeklyNotification()
        {
            try
            {
                NotificationHelper notify = new NotificationHelper();
                var data = await db.Database.SqlQuery<object>(@"CALL Email_agreement_renewal_daily").ToListAsync();
                foreach (var obj in data)
                {
                    EmailModel email = new EmailModel();
                    notify.sendMail(email);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal async Task<bool> monthlyNotification()
        {
            try
            {
                NotificationHelper notify = new NotificationHelper();
                var data = await db.Database.SqlQuery<object>(@"CALL Email_agreement_renewal_daily").ToListAsync();
                foreach (var obj in data)
                {
                    EmailModel email = new EmailModel();
                    notify.sendMail(email);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
