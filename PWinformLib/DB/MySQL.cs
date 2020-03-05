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
        private string connString;
        public void SetConnection(string servername, string dbname, string username, string password, string port)
        {
            connString = $@"Server={servername};Port={port};Database={dbname};Uid={username};Pwd={password}; convert zero datetime=True";
        }
        
        /// <summary>
        /// Get DataSet from MySql Query Asyncronously
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <returns>Returns DataSet.</returns>
        public Task<DataSet> AsyncDsSQL(String sql)
        {
            return Task.Run(() =>
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    return MySqlHelper.ExecuteDataset(con, sql);
                }
            });
        }

        /// <summary>
        /// Get DataSet from MySql Query
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <returns>Returns DataSet.</returns>
        public DataSet DsSql(string sql)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                return MySqlHelper.ExecuteDataset(con, sql);
            }
        }
        
        /// <summary>
        /// Get DataTable from MySql Query Asyncronously
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <returns>Returns DataSet.</returns>
        public Task<DataTable> AsyncDtSQL(String sql)
        {
            return Task.Run(() =>
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    return MySqlHelper.ExecuteDataset(con, sql).Tables[0];
                }
            });
        }

        /// <summary>
        /// Get DataTable from MySql Query Asyncronously
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <returns>Returns DataSet.</returns>
        public DataTable DtSql(String sql)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                return MySqlHelper.ExecuteDataset(con, sql).Tables[0];
            }
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
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    MySqlHelper.ExecuteNonQuery(con, sql);
                }
            });
        }

        /// <summary>
        /// Run Query Using Transaction
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <param name="limitter">Limitter Character.</param>
        /// <returns>No Returns.</returns>
        public void SqlTrans(string sql,string limitter)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand command = con.CreateCommand())
                {
                    con.Open();
                    MySqlTransaction mySqlTransaction = con.BeginTransaction();
                    command.Connection = con;
                    command.Transaction = mySqlTransaction;

                    try
                    {
                        string str1 = sql;
                        string[] separator = new string[1] { limitter };
                        foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                        {
                            command.CommandText = str2;
                            command.ExecuteNonQuery();
                        }
                        mySqlTransaction.Commit();
                    }
                    catch
                    {
                        mySqlTransaction.Rollback();
                        throw;
                    }
                }
            }
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
