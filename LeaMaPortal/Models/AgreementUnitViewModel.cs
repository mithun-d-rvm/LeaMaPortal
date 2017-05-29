using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementUnitViewModel
    {
        public AgreementUnitViewModel()
        {
            AgreementUnitList = new List<AgreementUnitViewModel>();
        }
        public int Id { get; set; }
        [DisplayName("Property ID")]
        public string Property_ID { get; set; }
        [DisplayName("Property Tawtheeq ID")]
        public string Property_ID_Tawtheeq { get; set; }
        [DisplayName("Property Name")]
        public string Properties_Name { get; set; }
        
        [DisplayName("Unit Tawtheeq ID")]
        public string Unit_ID_Tawtheeq { get; set; }
        [DisplayName("Unit Name")]
        public string Unit_Property_Name { get; set; }
        public List<AgreementUnitViewModel> AgreementUnitList { get; set; }
    }
}