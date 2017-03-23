using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    }
}