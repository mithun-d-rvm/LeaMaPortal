using LeaMaPortal.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Models
{
    public class PropertyViewModel 
    {
       public PropertyViewModel()
        {
            PropertiesdtList = new List<Propertiesdt>();
            Propertiesdt1List = new List<Propertiesdt1>();
            this.Status = "vacant";
            this.Status_unit = "vacant";
        }
        [StringLength(100)]
        [DisplayName("Property Type:")]
        public string Property_Flag { get; set; }

        [StringLength(100)]
        [DisplayName("Property Thwtheeq ID:")]
        public string Property_ID_Tawtheeq { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Property ID:")]
        public int Property_Id { get; set; }

        [StringLength(100)]
        [DisplayName("Property Name:")]
        public string Property_Name { get; set; }

        [StringLength(100)]
        [DisplayName("Compound:")]
        public string Compound { get; set; }

        [StringLength(100)]
        [DisplayName("Zone:")]
        public string Zone { get; set; }

        [StringLength(100)]
        [DisplayName("Sector:")]
        public string sector { get; set; }

        [StringLength(10)]
        [DisplayName("Plot Number:")]
        public string plotno { get; set; }

        [DisplayName("Owned by Registrant:")]
        public int? ownedbyregistrant { get; set; }

        [StringLength(100)]
        [DisplayName("Property Usage:")]
        public string Property_Usage { get; set; }

        [StringLength(100)]
        [DisplayName("Property Category:")]
        public string Property_Type { get; set; }

        [DisplayName("Commercial Villa:")]
        public int? Commercial_villa { get; set; }

        [StringLength(100)]
        [DisplayName("Street Name:")]
        public string Street_Name { get; set; }

        [StringLength(150)]
        [DisplayName("Address 1:")]
        public string Address1 { get; set; }

        [StringLength(150)]
        [DisplayName("Address 2:")]
        public string Address2 { get; set; }

        [StringLength(150)]
        [DisplayName("Address 3:")]
        public string Address3 { get; set; }

        [StringLength(150)]
        [DisplayName("Region:")]
        public string Region_Name { get; set; }

        [StringLength(150)]
        [DisplayName("Country:")]
        public string Country { get; set; }

        [StringLength(100)]
        [DisplayName("City:")]
        public string City { get; set; }

        [StringLength(100)]
        [DisplayName("State:")]
        public string State { get; set; }

        [StringLength(100)]
        [DisplayName("External ref no:")]
        public string Externalrefno { get; set; }

        [NotMapped]
        public SelectList ExternalrefnoList { get; set; }

        [DisplayName("Number of Floors:")]
        public int? Noofoffloors { get; set; }

        [DisplayName("Number of Units:")]
        public int? Noofunits { get; set; }

        [DisplayName("Built Area:")]
        public float? Builtarea { get; set; }

        [DisplayName("Plot Area:")]
        public float? Plotarea { get; set; }

        [DisplayName("Leasable Area:")]
        public float? Leasablearea { get; set; }

        [DisplayName("Common Area:")]
        public float? commonarea { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Completion Date:")]
        public DateTime? completion_Date { get; set; }

        [DisplayName("Property Value (in AED):")]
        public float? AEDvalue { get; set; }

        [NotMapped]
        public SelectList AEDvalueList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Purchased Date:")]
        public DateTime? Purchased_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Property Valued Date:")]
        public DateTime? Valued_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Available From:")]
        public DateTime? Vacant_Start_Date { get; set; }

        [DisplayName("Min.Rental Rate/Month (in AED):")]
        public float? Rental_Rate_Month { get; set; }

        [StringLength(1073741823)]
        [DisplayName("Comments:")]
        public string Comments { get; set; }

        [StringLength(100)]
        [DisplayName("Main Property Tawtheeq ID:")]
        public string Ref_unit_Property_ID_Tawtheeq { get; set; }

        [DisplayName("Main Property ID:")]
        public int? Ref_Unit_Property_ID { get; set; }

        [StringLength(100)]
        [DisplayName("Main Property Name:")]
        public string Ref_Unit_Property_Name { get; set; }

        [StringLength(100)]
        [DisplayName("Unit Tawteeq ID:")]
        public string Unit_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        [DisplayName("Units Name:")]
        public string Unit_Property_Name { get; set; }

        [NotMapped]
        public SelectList Unit_Property_NameList { get; set; }

        [StringLength(100)]
        [DisplayName("External Ref No:")]
        public string Externalrefno_unit { get; set; }

        [DisplayName("Property Value (in AED):")]
        public float? AEDvalue_unit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Purchased Date:")]
        public DateTime? Purchased_date_unit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Property Value Date:")]
        public DateTime? Valued_Date_unit { get; set; }

        [StringLength(100)]
        [DisplayName("Occupancy Status (Unit):")]
        public string Status_unit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Available From (Unit):")]
        public DateTime? Vacant_Start_Date_Unit { get; set; }

        [DisplayName("Min. Rental Rate / Month (in AED) (Unit) : ")]
        public float? Rental_Rate_Month_unit { get; set; }

        [StringLength(100)]
        [DisplayName("Floor Number:")]
        public string Floorno { get; set; }

        [StringLength(100)]
        [DisplayName("FLoor Level:")]
        public string Floorlevel { get; set; }

        [StringLength(100)]
        [DisplayName("Units Usage:")]
        public string Property_Usage_unit { get; set; }

        [NotMapped]
        public SelectList Property_Usage_unitList { get; set; }

        [StringLength(100)]
        [DisplayName("Property Category (Unit):")]
        public string Property_Type_unit { get; set; }

        [NotMapped]
        public SelectList Property_Type_unitList { get; set; }

        [DisplayName("Total Area:")]
        public float? Total_Area { get; set; }

        [DisplayName("Unit Common Area:")]
        public float? Unit_Common_Area { get; set; }

        [DisplayName("Common Area:")]
        public float? Common_Area { get; set; }

        [DisplayName("Parking Number:")]
        public int? Parkingno { get; set; }

        [StringLength(1073741823)]
        [DisplayName("Comments (Unit):")]
        public string Unitcomments { get; set; }

        [StringLength(100)]
        [DisplayName("Occupancy Status:")]
        public string Status { get; set; }

        [StringLength(100)]
        [DisplayName("Caretaker Name:")]
        public string Caretaker_Name { get; set; }

        [DisplayName("Caretaker ID:")]
        public int? Caretaker_ID { get; set; }

        [NotMapped]
        public SelectList Caretaker_IDList { get; set; }

        [DisplayName("Using by Hameem and not for lease:")]
        public int? Company_occupied_Flag { get; set; }

        public int? Accyear { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Createddatetime { get; set; }

        [StringLength(50)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public string propertiesdt { get; set; }
        public string propertiesdt1 { get; set; }
        public List<Propertiesdt> PropertiesdtList { get; set; }
        public List<Propertiesdt1> Propertiesdt1List { get; set; }
    }
    public class Propertiesdt
    {
        public int Property_Id { get; set; }
        [StringLength(100)]
        public string Property_ID_Tawtheeq { get; set; }
        [StringLength(100)]
        public string Unit_ID_Tawtheeq { get; set; }
        [StringLength(150)]
        public string Facility_id { get; set; }
        [StringLength(150)]
        public string Facility_Name { get; set; }
        public string Numbers_available { get; set; }
    }

    public class Propertiesdt1
    {
        public int Property_Id { get; set; }
        [StringLength(100)]
        public string Property_ID_Tawtheeq { get; set; }
        [StringLength(100)]
        public string Unit_ID_Tawtheeq { get; set; }
        public string Utility_id { get; set; }
        [StringLength(150)]
        public string Utility_Name{ get; set; }
    }

}