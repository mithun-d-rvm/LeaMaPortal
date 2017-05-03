using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Model
{
    public class EmailModel
    {
        public string from { get; set; }
        public string toList { get; set; }
        public string ccList { get; set; }
        public string sub { get; set; }
        public string body { get; set; }
    }
}
