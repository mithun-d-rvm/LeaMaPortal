using LeaMaPortal.DBContext;
using System.Threading.Tasks;
using System.Data.Entity;
using System;
using Notification.Model;

namespace Notification
{
    public class NotificationDAL
    {
        private LeamaEntities db = new LeamaEntities();
        internal async Task<bool> dailyNotification()
        {
            try
            {
                NotificationHelper obj = new NotificationHelper();
                var data = await db.Database.SqlQuery<object>(@"CALL Email_agreement_renewal_daily").ToListAsync();
                EmailModel email = new EmailModel();
                obj.sendMail(email);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
