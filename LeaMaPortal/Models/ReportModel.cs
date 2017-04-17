using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class VacancyReportModel
    {

        [DisplayName("Group:")]
        public string Group { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("From Date:")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("To Date:")]
        public DateTime? ToDate { get; set; }

        [DisplayName("Filter by:")]
        public string Filterby { get; set; }

        [DisplayName("Value:")]
        public string Value { get; set; }

        [DisplayName("Aging Filter:")]
        public string AgingFilter { get; set; }

        [DisplayName("From:")]
        public int AgingFrom { get; set; }

        [DisplayName("To:")]
        public int AgingTo { get; set; }

        [DisplayName("Rental Amount Filter:")]
        public string RentalAmountFilter { get; set; }

        [DisplayName("From:")]
        public int RentalAmountFrom { get; set; }

        [DisplayName("To:")]
        public int RentalAmountTo { get; set; }

        [DisplayName("Created User")]
        public string CreatedUser { get; set; }
    }
    public class VacantRegionReportModel
    {
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public long Noof_properties { get; set; }
        public string Aging_Range { get; set; }
        public string Amount_range { get; set; }
        public double Loss_Amt { get; set; }
    }

    public class VacantCaretakerReportModel
    {
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public long Noof_properties { get; set; }
        public string Aging_Range { get; set; }
        public double Loss_Amt { get; set; }
        public string user { get; set; }
    }

}