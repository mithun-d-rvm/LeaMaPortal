using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Model
{
    class EmailModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string ToList { get; set; }
        public string CCList { get; set; }
        public string BCCList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
