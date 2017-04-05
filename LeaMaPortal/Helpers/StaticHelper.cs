using LeaMaPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Helpers
{
    public class StaticHelper
    {
        //checkList Values        
        public const string CHECKlIST_NEWCONTRACT = "New Contract";
        public const string CHECKlIST_CONTRACTRENEWAL = "Contract Renewal";
        public const string CHECKlIST_CONTRACTCLOSURE = "Contract Closure";
        //property type master values
        public const string PROPERTYTYPE_PROPERTY = "Property";
        public const string PROPERTYTYPE_UNIT = "Unit";
        //Colour for slab master
        public const string SLAB_RED = "Red";
        public const string SLAB_AMBER = "Amber";
        public const string SLAB_GREEN = "Green";
        //Residence type
        public const string SLAB_UAE = "UAE";
        public const string SLAB_NONUAE = "Non UAE";

        public const byte CHECKLIST_DROPDOWN = 1;
        public const byte PROPERTYTYPE_DROPDOWN = 2;
        public const byte METER_DROPDOWN = 3;
        public const byte SLAB_COLOUR_DROPDOWN = 4;
        public const byte SLAB_RESIDENCE_DROPDOWN = 5;
        public const byte PROPERTY = 6;
        public const byte UNIT = 7;
        public const byte PROPERTYCATEGORY_DROPDOWN = 8;

        public const string PROPERTY_RESIDENTIAL = "residential";
        public const string PROPERTY_COMMERCIAL = "commercial";
        public const string PROPERTY_INDUSTRIAL = "industrial";
        public const string PROPERTY_RESIDENTIAL_COMMERCIAL = "residential-commercial";
        public const string PROPERTY_INDUSTRIAL_COMMERCIAL = "industrial-commercial";

        public const string UNIT_RESIDENTIAL = "residential";
        public const string UNIT_COMMERCIAL = "commercial";

        public const string PROPERTYCATEGORY_CATEGORY_1 = "category 1";
        public const string PROPERTYCATEGORY_CATEGORY_2 = "category 2";
        public const string PROPERTYCATEGORY_CATEGORY_3 = "category 3";

        private static List<OptionModel> GetCheckListDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Name = CHECKlIST_NEWCONTRACT });
            options.Add(new OptionModel { Name = CHECKlIST_CONTRACTRENEWAL });
            options.Add(new OptionModel { Name = CHECKlIST_CONTRACTCLOSURE });
            return options;
        }
        private static List<OptionModel> GetPropertyTypeDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Name = PROPERTYTYPE_PROPERTY });
            options.Add(new OptionModel { Name = PROPERTYTYPE_UNIT });
            return options;
        }
        private static List<OptionModel> GetPropertyDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Name = PROPERTY_RESIDENTIAL });
            options.Add(new OptionModel { Name = PROPERTY_COMMERCIAL });
            options.Add(new OptionModel { Name = PROPERTY_INDUSTRIAL });
            options.Add(new OptionModel { Name = PROPERTY_RESIDENTIAL_COMMERCIAL });
            options.Add(new OptionModel { Name = PROPERTY_INDUSTRIAL_COMMERCIAL });
            return options;
        }
        private static List<OptionModel> GetPropertyCategoryDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Name = PROPERTYCATEGORY_CATEGORY_1 });
            options.Add(new OptionModel { Name = PROPERTYCATEGORY_CATEGORY_2 });
            options.Add(new OptionModel { Name = PROPERTYCATEGORY_CATEGORY_3 });
            return options;
        }
        private static List<OptionModel> GetUnitDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Name = UNIT_RESIDENTIAL });
            options.Add(new OptionModel { Name = UNIT_COMMERCIAL });
            return options;
        }
        private static List<OptionModel> GetMeterDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            for (byte i = 1; i <= 31; i++)
            {
                options.Add(new OptionModel { Id = i });
            }            
            return options;
        }
        private static List<OptionModel> GetColourDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Name = SLAB_RED });
            options.Add(new OptionModel { Name = SLAB_GREEN });
            options.Add(new OptionModel { Name = SLAB_AMBER });
            return options;
        }
        private static List<OptionModel> GetResidenceDropdown()
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Name = SLAB_UAE });
            options.Add(new OptionModel { Name = SLAB_NONUAE });
            return options;
        }
        public static List<OptionModel> GetStaticData(byte dataModelType)
        {
            switch (dataModelType)
            {
                case CHECKLIST_DROPDOWN:
                    return GetCheckListDropdown();
                case PROPERTYTYPE_DROPDOWN:
                    return GetPropertyTypeDropdown();
                case METER_DROPDOWN:
                    return GetMeterDropdown();
                case SLAB_COLOUR_DROPDOWN:
                    return GetColourDropdown();
                case SLAB_RESIDENCE_DROPDOWN:
                    return GetResidenceDropdown();
                case PROPERTY:
                    return GetPropertyDropdown();
                case UNIT:
                    return GetUnitDropdown();
                case PROPERTYCATEGORY_DROPDOWN:
                    return GetPropertyCategoryDropdown();
                default:
                    return new List<OptionModel>();
            }
        }
    }
}