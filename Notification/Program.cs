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
            NotificationHelper obj = new NotificationHelper();
            obj.sendMail();                     
        }
    }
}
