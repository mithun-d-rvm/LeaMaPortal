using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NotificationDAL obj = new NotificationDAL();
                obj.dailyNotification();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
