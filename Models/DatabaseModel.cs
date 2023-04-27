using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace GameFinderAppV2.Models
{
    public class DatabaseModel : DbContext
    {
        private static string _connString = "Server=localhost\\SQLEXPRESS;Database=Game_Database_V2;Trusted_Connection=True;";
        public DatabaseModel() : base("Server=localhost\\SQLEXPRESS;Database=Game_Database_V2;Trusted_Connection=True;") { }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<PublisherModel> Publishers { get; set; }
        public List<DBDataModel> getDatabaseData(Dictionary<string, string> inParams, string table)
        {
            List<DBDataModel> values = new List<DBDataModel>();
            string sqlQuery = "select * from " + table + " where ";

            foreach (string parameter in inParams.Keys)
            {
                string value = inParams.GetValueOrDefault(parameter);
                if (isString(value))
                {
                    value = "'" + value + "'";   
                }
                sqlQuery += parameter + " = " + value + " and ";
            }
            sqlQuery = sqlQuery.Remove(sqlQuery.Length - 5, 4);

            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();

            SqlCommand command = new SqlCommand(sqlQuery, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PropertyInfo[] fields 
                        = WorkerModel.getFields(Type.GetType(this.GetType().Namespace + "." + table.Substring(0, table.Length - 1)));
                    int size = fields.Length;

                    DBDataModel row = new DBDataModel();
                    for (int i = 1; i < fields.Length; i++)
                    {
                        row.param.Add(fields[i - 1].Name, reader.GetValue(i).ToString());
                       
                    }

                    values.Add(row);

                }
            }

            conn.Close();
            return values;
        }

        private bool isString(string parameter)
        {
            return !Regex.Match(parameter, "^[0-9.]+$").Success;
        }

    }
}
