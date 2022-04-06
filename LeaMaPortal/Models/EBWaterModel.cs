using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class EBWaterModel
    {
        public int Id { get; set; }
        [Display(Name = "Bill Entry No")]
        public int BillEnteryNo { get; set; }
        [Display(Name = "Bill Entry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BillEntryDate { get; set; }
        public string UtilityId { get; set; }
        [Display(Name = "Utility Name")]

        public string UtilityName { get; set; }
        
        public int? SupplierId { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
       
        public int MyProperty { get; set; }
        public List<EBWaterDetailsModel> Details { get; set; }
    }
    public class EBWaterDetailsModel
    {
        public int? RefNo { get; set; }
        public string MeterNo { get; set; }
        public string PropertyId { get; set; }
        public string UnitId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReadingDate { get; set; }
        public int? BillNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BillDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        public string MeterReadingNo { get; set; }
        public float? TotalUnits { get; set; }
        public int? DayOfUse { get; set; }
        public float? Amount { get; set; }
        public float? Rate { get; set; }
    }
}