using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class EBWaterModel
    {
        public int Id { get; set; }
        public int BillEnteryNo { get; set; }
        public DateTime? BillEntryDate { get; set; }
        public string UtilityId { get; set; }
        public string UtilityName { get; set; }
        public int? SupplierId { get; set; }
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
        public DateTime? ReadingDate { get; set; }
        public int? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string MeterReadingNo { get; set; }
        public float? TotalUnits { get; set; }
        public int? DayOfUse { get; set; }
        public float? Amount { get; set; }
        public float? Rate { get; set; }
    }
}