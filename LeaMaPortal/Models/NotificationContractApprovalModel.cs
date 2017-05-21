using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class NotificationContractApprovalModel
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string TenantName { get; set; }
        public string UnitName { get; set; }
        public int AgreementNo { get; set; }
        public int? isApproved { get; set; }
    }
}