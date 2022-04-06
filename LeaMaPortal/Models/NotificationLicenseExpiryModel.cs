using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class NotificationLicenseExpiryModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime issuedate { get; set; }
        public DateTime Expirydate { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
         
        public string ISSDAT { get; set; }
        public string EXPDAT { get; set; }

    }
}