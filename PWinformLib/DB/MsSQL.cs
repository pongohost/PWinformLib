using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using PWinformLib.Preloader;
using PWinformLib.UI;

namespace PWinformLib.DB
{
    public class MsSQL
    {
        private string ConString = "";
        
        public void SetConnection(String servername, String dbname, String username, String password, String port)
        {
            ConString = "Server=" + servername + ","+ port + ";Database=" + dbname + ";User Id=" + username + ";Password=" + password + ";";
        }

        public void InsertMultiple(string tableName, string data, string pemisah)
        {
            string str1 = "INSERT INTO " + tableName + " VALUES ";
            string str2 = data;
            string[] separator = new string[1] { pemisah };
            foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                str1 = str1 + " (" + str3 + "),";
            this.SqlTrans(str1.Remove(str1.Length - 1), "||||");
        }
        
        public Task insertMultipleAsync(string kueryAwal, string data, string pemisah)
        {
            return Task.Run((Action)(() => InsertMultiple(kueryAwal, data, pemisah)));
        }

        public void BulkInsert(string TblName, DataTable data, List<string[]> columnMapping = null,
            SqlBulkCopyOptions options = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction,
            SqlTransaction SqlTrans = null)
        {
            using (SqlConnection connection = new SqlConnection(this.ConString))
            {
                connection.Open();
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, options, SqlTrans))
                {
                    try
                    {
                        sqlBulkCopy.DestinationTableName = TblName;
                        foreach (DataColumn column in (InternalDataCollectionBase)data.Columns)
                            sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                        sqlBulkCopy.WriteToServer(data);
                    }
                    catch
                    {
                        connection.Close();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public Task BulkInsertAsync(string TblName, DataTable data, List<string[]> columnMapping = null,
          SqlBulkCopyOptions options = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction,
          SqlTransaction SqlTrans = null)
        {
            return Task.Run((Action)(() => this.BulkInsert(TblName, data, columnMapping, options, SqlTrans)));
        }

        public DataSet DsSql(string perintah)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(this.ConString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(perintah, connection))
                    {
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                        while (!sqlDataReader.IsClosed)
                        {
                            DataTable table = new DataTable();
                            table.Load((IDataReader)sqlDataReader);
                            dataSet.Tables.Add(table);
                        }
                    }
                }
                catch
                {
                    connection.Close();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return dataSet;
        }

        public Task<DataSet> DsSqlAsync(string sql)
        {
            return Task.Run<DataSet>((Func<DataSet>)(() => this.DsSql(sql)));
        }

        public DataTable DtSql(string sql)
        {
            return this.DsSql(sql).Tables[0];
        }

        public Task<DataTable> DtSqlAsync(string sql)
        {
            return Task.Run<DataTable>((Func<DataTable>)(() => this.DtSql(sql)));
        }

        public int SimpleSql(string query)
        {
            using (SqlConnection connection = new SqlConnection(this.ConString))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();
                    return sqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    connection.Close();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Task<int> SimpleSqlAsync(string sql)
        {
            return Task.Run<int>((Func<int>)(() => this.SimpleSql(sql)));
        }

        public void SqlTrans(string query, string limitter)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.ConString;
            using (new SqlCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string str = query;
                    string[] separator = new string[1] { limitter };
                    foreach (string cmdText in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                        new SqlCommand(cmdText, connection, transaction).ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    connection.Close();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Task SqlTransAsync(string sql, string separator)
        {
            return Task.Run((Action)(() => this.SqlTrans(sql, separator)));
        }

        public void insertDgv(DataGridView dataGridViewv, string kuery, string[] columnName)
        {
            string query = "";
            for (int index1 = 0; index1 < dataGridViewv.Rows.Count; ++index1)
            {
                string str = query + kuery + " VALUES (";
                for (int index2 = 0; index2 < columnName.Length; ++index2)
                {
                    str = str + "'" + dataGridViewv.Rows[index1].Cells[columnName[index2]].Value + "'";
                    if (index2 < columnName.Length - 1)
                        str += ", ";
                }
                query = str + ") ";
                if (index1 < dataGridViewv.Rows.Count - 1)
                    query += "[|]";
            }
            this.SqlTrans(query, "[|]");
        }

        public Task insertDgvAsync(DataGridView dataGridViewv, string query, string[] columnName)
        {
            return Task.Run((Action)(() => this.insertDgv(dataGridViewv, query, columnName)));
        }

        public async void insertLog(string id, string modul, string pesan, string ip, string hostname, int sessi)
        {
            using (SqlConnection sqlcon = new SqlConnection(this.ConString))
            {
                using (SqlCommand cmd = new SqlCommand("EXEC insertLog @ident = '" + id + "',@modul='" + modul + "',@messg='" + Helper.AddSlash(pesan) + "',@ip='" + ip + "',@hostn='" + hostname + "',@sessi=" + (object)sessi, sqlcon))
                {
                    try
                    {
                        await cmd.Connection.OpenAsync();
                        int num = await cmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        notification.Error("Connection setup", "Connect to DB Failed " + ex.Message.ToString(), false);
                    }
                }
            }
        }
        
        public async void DgvFromSqlAsync(DataGridView dgv, string sql)
        {
            Preloaderani.addSpinnLoad((Control)dgv, "", "BoxSwap", 77, 77, (Image)null);
            dgv.DataSource = (object)(await this.DsSqlAsync(sql)).Tables[0];
            Preloaderani.remSpinnLoad((Control)dgv);
        }

        public void TesConnectionN(string servername, string dbname, string username, string password, string port)
        {
            try
            {
                this.TesConnection(servername, dbname, username, password, port);
                notification.Info("Connection setup", "Successfully connected to DB.", false);
            }
            catch (Exception ex)
            {
                notification.Error("Connection setup", "Connect to DB Failed " + ex.Message.ToString(), false);
            }
        }

        public async void TesConnection(string servername, string dbname, string username, string password, string port)
        {
            using (SqlConnection conn = new SqlConnection("Server=" + servername + "," + port + ";Database=" + dbname + ";User Id=" + username + ";Password=" + password + ";"))
            {
                using (SqlCommand cmd = new SqlCommand("select 1 as hasil", conn))
                {
                    await cmd.Connection.OpenAsync();
                    int num = await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
