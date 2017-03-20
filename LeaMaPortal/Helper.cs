using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal
{
    public static class Helper
    {
        public static List<MySqlParameter> GetMySqlParameters<T>(T model)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            foreach (var property in model.GetType().GetProperties().ToList())
            {
                parameters.Add(new MySqlParameter("P" + property.Name, property.GetValue(model)));
            }
            return parameters;
        }

    }
}