using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class TcaStatusViewModel
    {
        
        public int Id { get; set; }
        public int Agreement_No { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public int Properties_ID { get; set; }
        public string Properties_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Renewal_Close_Flag { get; set; }
        public string Createduser { get; set; }

    }

    public class TcaStatusDisplayModel
    {

        public int Id { get; set; }
        public int Agreement_No { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public int Properties_ID { get; set; }
        public string Properties_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Renewal_Close_Flag { get; set; }
        public string Createduser { get; set; }

        public string AgreementDate { get; set; }
        public string Tenant_Type { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }

    }
}