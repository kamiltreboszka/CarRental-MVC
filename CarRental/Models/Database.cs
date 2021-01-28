using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public static class Database
    {
        private static DbConnection connection;
        private static Dictionary<Type, string> connectionStrings;

        static Database()
        {
            connectionStrings = new Dictionary<Type, string>();
            connectionStrings[typeof(NpgsqlConnection)] = "Host=localhost;Port=5432;Username=postgres;Database=CarRental";
        }

        public static DbConnection GetConnection(Type type)
        {
            if (connection == null || !type.IsInstanceOfType(connection))
            {
                if (connection != null)
                    try
                    {
                        connection.Close();
                    }
                    catch
                    {

                    }

                connection = (DbConnection)Activator.CreateInstance(type, connectionStrings[type]);

                connection.Open();
            }
            return connection;
        }

        public static void AddParameter(this DbCommand cmd, string name, object value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            cmd.Parameters.Add(param);
        }
    }
}