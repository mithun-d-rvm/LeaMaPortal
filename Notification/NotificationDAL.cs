using LeaMaPortal.DBContext;
using System.Threading.Tasks;
using System.Data.Entity;
using System;
using Notification.Model;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

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
                                toList = s.toid,
                                ccList = s.cc,
                                sub = s.Subject,
                                body = s.body
                            }).ToListAsync();
                Type myType = data.GetType();

                foreach (var obj in data)
                {                    
                    notify.sendMail(obj);
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
