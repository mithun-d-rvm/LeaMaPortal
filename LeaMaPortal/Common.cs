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
        public const int DefaultMaster = 11;
        public const string TenantIndividualDocumentContainer = "Documents/TenantIndividual/";
    }
}