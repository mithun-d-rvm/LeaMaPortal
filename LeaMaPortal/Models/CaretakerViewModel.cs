using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class CaretakerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Caretaker Id")]
        public int Caretaker_id { get; set; }
        [DisplayName("Caretaker Name")]
        public string Caretaker_Name { get; set; }
        //[DisplayName("Father Name")]
        //public string Father_Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        public DateTime? Dob { get; set; }

        [DisplayName("Address1")]
        public string Address1 { get; set; }
        [DisplayName("Address2")]
        public string Address2 { get; set; }
        //[DisplayName("Address3")]
        //public string Address3 { get; set; }
        [DisplayName("Region Name")]
        public string Region_Name { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        //[DisplayName("Area")]
        //public string Area { get; set; }
        // [DisplayName("Pincode")]
        [DisplayName("Emirates Id")]
        public string Pincode { get; set; }
        [DisplayName("Phone number")]
        public string Phoneno { get; set; }
        [DisplayName("Email ID")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Joining")]
        public DateTime? Doj { get; set; }
       
    }
}