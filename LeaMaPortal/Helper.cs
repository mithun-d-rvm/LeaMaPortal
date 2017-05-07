using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace LeaMaPortal
{
    public static class Helper
    {
        public static object[] GetMySqlParameters<T>(T model, string flag, string CreatedUser = null, MySqlParameter optionalParam = null)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@PFlag", flag));
            if (optionalParam != null)
                parameters.Add(optionalParam);

            foreach (var property in model.GetType().GetProperties().ToList())
            {
                if (property.Name == "Createduser")
                    parameters.Add(new MySqlParameter("@P" + property.Name, CreatedUser));
                else
                    parameters.Add(new MySqlParameter("@P" + property.Name, property.GetValue(model)));
            }

            return parameters.ToArray();
        }
        public static string GetTcaMySqlParametersNames<T>(T model, string flag, string CreatedUser = null, MySqlParameter optionalParam = null)
        {
            string paramstr= "@PFlag";
            foreach (var property in model.GetType().GetProperties().ToList())
            {
                if (property.Name == "AgreementUtilityList" || property.Name == "AgreementPdcList" ||
                   property.Name == "agreementDocumentList" || property.Name == "AgreementUnitList" ||
                   property.Name == "agreementDocumentExistList" || property.Name == "agreementFacilityList"
                   ||property.Name== "AgreementCheckList")
                    continue;
                else
                paramstr += ", @P" + property.Name;
            }

            return paramstr;
        }
        public static object[] GetTcaMySqlParameters<T>(T model, string flag, string CreatedUser = null, MySqlParameter optionalParam = null)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@PFlag", flag));
            if (optionalParam != null)
                parameters.Add(optionalParam);

            foreach (var property in model.GetType().GetProperties().ToList())
            {
                if (property.Name == "AgreementUtilityList" || property.Name == "AgreementPdcList" || 
                    property.Name == "agreementDocumentList" || property.Name == "AgreementUnitList" ||
                    property.Name== "agreementDocumentExistList" || property.Name== "agreementFacilityList"
                     || property.Name == "AgreementCheckList")
                    continue;
                if (property.Name == "Createduser")
                    parameters.Add(new MySqlParameter("@P" + property.Name, CreatedUser));
                else
                    parameters.Add(new MySqlParameter("@P" + property.Name, property.GetValue(model)));
            }

            return parameters.ToArray();
        }


        public static bool CheckDirectory(string name)
        {
            try
            {
                var path = string.Format("{0}Documents", AppDomain.CurrentDomain.BaseDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = string.Format("{0}Documents\\" + name, AppDomain.CurrentDomain.BaseDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}