using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal
{
    public static class Common
    {
        public static List<string> Title = new List<string>() {"MR.", "MRS.","MS." };
        public const string DefaultTitle= "MR.";
        //public static List<string> City = new List<string>() { "Abudhabi", "Sharja" };
        public static List<string> Profession = new List<string>() { "Engineer", "Teacher", "Shop keeper","Doctor","Farmer" };

        public const string INSERT = "INSERT", UPDATE = "UPDATE", DELETE = "DELETE", SELECT= "SELECT",View = "View";
        public const int DefaultMaster = 10;
        public const string TenantIndividualDocumentContainer = "Documents/TenantIndividual/";

        //tenant company
        public static List<string> TenantType = new List<string>() { "Government","Person", "Company" };
        public static List<string> Emirate = new List<string>() { "Default" };
        public static List<string> ComapanyActivity = new List<string>() { "Activity1" };
        public static List<string> Issuance_authority = new List<string>() { "List-1" };
        public const string TenantCompanyDocumentContainer = "Documents/TenantCompany/";

    }
}