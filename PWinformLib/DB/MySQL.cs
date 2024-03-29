﻿using System;
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

        /// <summary>
        /// Set Connection string
        /// </summary>
        /// <param name="servername">Server Name.</param>
        /// <param name="dbname">db name.</param>
        /// <param name="username">Username.</param>
        /// <param name="password">password.</param>
        /// <param name="port">port.</param>
        /// <returns>Returns DataSet.</returns>
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
            return Task.Run<DataSet>((Func<DataSet>)(() => this.DsSql(sql)));
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
            return Task.Run<DataTable>((Func<DataTable>)(() => this.DtSql(sql)));
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
        /// Run No Query Async
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <returns>No Returns.</returns>
        public Task AsyncNonQuery(String sql)
        {
            /*return Task.Run(() =>
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    MySqlHelper.ExecuteNonQuery(con, sql);
                }
            });*/
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                return MySqlHelper.ExecuteDatasetAsync(con, sql);
            }
        }

        /// <summary>
        /// Run No Query Async
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <param name="listPar">List Parametes.</param>
        /// <returns>No Returns.</returns>
        public Task InsertParam(String sql,List<String> listPar)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                string strQ = " VALUES (";
                MySqlParameter[] param = new MySqlParameter[listPar.Count];
                for (int i = 0; i < listPar.Count; i++)
                {
                    param[i] = new MySqlParameter("@a"+i,listPar[i]);
                    strQ = strQ + "@a"+i+",";
                }

                sql = sql + strQ.Remove(strQ.Length-1,1)+")";
                Console.Out.WriteLine(sql);
                return MySqlHelper.ExecuteDatasetAsync(con, sql,param);
            }
        }

        /// <summary>
        /// Run No Query Async
        /// </summary>
        /// <param name="sql">SQL Query to execute.</param>
        /// <param name="listPar">List Parametes.</param>
        /// <returns>No Returns.</returns>
        public Task UpdateParam(String sql, List<KeyValuePair<string,string>> listPar,String whereVal)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                string strQ = "";
                MySqlParameter[] param = new MySqlParameter[listPar.Count];
                for (int i = 0; i < listPar.Count; i++)
                {
                    param[i] = new MySqlParameter("@a"+i, listPar[i].Value);
                    strQ = strQ + listPar[i].Key + " = @a"+i+",";
                }

                sql = sql + strQ.Remove(strQ.Length-1, 1)+whereVal;
                Console.Out.WriteLine(sql);
                return MySqlHelper.ExecuteDatasetAsync(con, sql, param);
            }
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
