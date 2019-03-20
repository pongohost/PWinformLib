using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PWinformLib.UI;

namespace PWinformLib.DB
{
    public class MySQL
    {
        static MySqlConnection sqlcon = new MySqlConnection();
        public void SetConnection(String servername, String dbname, String username, String password, String port)
        {
            if(sqlcon.State == ConnectionState.Open)
                sqlcon.Close();
            sqlcon.ConnectionString = $@"Server={servername};Port={port};Database={dbname};Uid={username};Pwd={password}";
        }

        /// <summary>
        /// Get DataSet from MySql Query
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <returns>Returns DataSet.</returns>
        public Task<DataSet> AsyncDsSQL(String sql)
        {
            return Task.Run(() =>
            {
                DataSet ds = MySqlHelper.ExecuteDataset(sqlcon, sql);
                return ds;
            });
        }

        /// <summary>
        /// Run No Query
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <returns>No Returns.</returns>
        public Task AsyncNonQuery(String sql)
        {
            return Task.Run(() =>
            {
                if(sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                MySqlHelper.ExecuteNonQuery(sqlcon, sql);
            });
        }

        /// <summary>
        /// Create Bulk Insert Statement
        /// </summary>
        /// <param name="query">SQL Query to execute.</param>
        /// <param name="dtTable">DataTable to execute.</param>
        /// <returns>Returns string insert query.</returns>
        /// <example> 
        /// This sample shows how to call the <see cref="CreateInserBulkStatement"/> method.
        /// <code>
        /// class TestClass 
        /// {
        ///     static int Main() 
        ///     {
        ///         return CreateInserBulkStatement("INSERT INTO User (FirstName, LastName) VALUES ",dt);
        ///     }
        /// }
        /// </code>
        /// </example>
        public string CreateInserBulkStatement(string query,DataTable dtTable)
        {
            StringBuilder sCommand = new StringBuilder(query);
            List<string> Rows = new List<string>();
            string format = "(";
            for (int i = 0; i < dtTable.Columns.Count; i++)
            {
                format = format + $"'{{{i}}}',";
            }
            format = format.Substring(0, format.Length - 1) + ")";
            foreach (DataRow itm in dtTable.Rows)
            {
                Rows.Add(string.Format(format, itm.ItemArray));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");
            return sCommand.ToString();
        }
    }
}
