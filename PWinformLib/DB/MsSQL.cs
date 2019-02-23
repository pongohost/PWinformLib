using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using PWinformLib.Preloader;
using PWinformLib.UI;

namespace PWinformLib
{
    public class MsSQL
    {
        static SqlConnection sqlcon = new SqlConnection();
        static int mm1 = 1;
        static int[] mm2 = new int[10] { 0,0,0,0,0,0,0,0,0,0 };

        public static void setpar(String servername, String dbname, String username, String password, String port)
        {
            parameter.Server = servername;
            parameter.Db = dbname;
            parameter.Usern = username;
            parameter.Pass = password;
            parameter.Port = port;
        }

        public static void setconnection()
        {
            sqlcon.ConnectionString = "Server=" + parameter.Server + ","+ parameter.Port + ";Database=" + parameter.Db + ";User Id=" + parameter.Usern + ";Password=" + parameter.Pass + ";";
        }

        public static void kuerisql(String perintah,String judul, String pesan)
        {
            SqlCommand command = new SqlCommand(perintah, sqlcon);
            sqlcon.Open();
            try
            {
                command.ExecuteNonQuery();
                notification.Ok(judul, pesan);
            }
            catch (SqlException exx)
            {
                notification.Error("Query Gagal", exx.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        //===============================================================================================
        public static void insertMultiple(String kueryAwal,String data,String pemisah)
        {
            String hasil = insertMultipleAct(kueryAwal, data, pemisah);
            if(hasil.Equals("OK"))
                notification.Ok("Simpan Data", "Data Berhasil di Simpan.");
            else
                notification.Error("Query Gagal", hasil);

        }
        public static String insertMultipleAct(String kueryAwal, String data, String pemisah)
        {
            string StrQuery = "";
            String status = "";
            using (SqlCommand comm = new SqlCommand())
            {
                sqlcon.Open();
                try
                {
                    String[] data2 = data.Split(new[] { pemisah }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < data2.Length; i++)
                    {
                        StrQuery = kueryAwal + " VALUES (";
                        StrQuery = StrQuery + data2[i] + ");";
                        comm.CommandText = StrQuery;
                        comm.Connection = sqlcon;
                        comm.ExecuteNonQuery();
                    }
                }
                catch (SqlException exx)
                {
                    status = exx.Message + " - " + StrQuery;
                }
                finally
                {
                    sqlcon.Close();
                    status = "OK";
                }
            }
            return status;
        }
        //==========================================================================================================
        public static DataTable queryTransaction(String kuery, String pemisah)
        {
            string StrQuery = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("Status", typeof(String));
            dt.Columns.Add("Pesan", typeof(String));
            SqlTransaction transaction;
            using (SqlCommand comm = new SqlCommand())
            {
                sqlcon.Open();
                transaction = sqlcon.BeginTransaction();
                try
                {
                    String[] data2 = kuery.Split(new[] { pemisah }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < data2.Length; i++)
                    {
                        new SqlCommand(data2[i], sqlcon, transaction).ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (SqlException exx)
                {
                    //notification.Error("Query Gagal", exx.Message + " - " + StrQuery);
                    dt.Rows.Add("error", exx.Message + " - " + StrQuery);
                }
                finally
                {
                    //notification.Ok("Simpan Data", "Data Berhasil di Simpan.");
                    dt.Rows.Add("ok", "Data Berhasil di Simpan");
                    sqlcon.Close();
                }
            }

            return dt;
        }

        public static DataSet insertAllDatagrid(DataGridView dgv, string kuery, string[] namaKolom, string kueriHapus)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(simplemssql(kueriHapus));
            ds.Tables.Add(insertAllDatagrid(dgv, kuery, namaKolom));
            return ds;
        }

        public static DataTable insertAllDatagrid(DataGridView dgv, string kuery, string[] namaKolom)
        {
            DataTable table1 = new DataTable("insertAllDatagrid");
            table1.Columns.Add("status");
            table1.Columns.Add("Note");
            using (SqlConnection conn = new SqlConnection("Server=" + parameter.Server + "," + parameter.Port + ";Database=" + parameter.Db + ";User Id=" + parameter.Usern + ";Password=" + parameter.Pass + ";"))
            {
                try
                {
                    conn.Open();
                    string StrQuery = kuery + " VALUES";
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        StrQuery = StrQuery + " (";
                        for (int ii = 0; ii < namaKolom.Length; ii++)
                        {
                            StrQuery = StrQuery + "'"+dgv.Rows[i].Cells[namaKolom[ii].ToString()].Value+"'";
                            if (ii < namaKolom.Length - 1)
                                StrQuery = StrQuery + ", ";
                        }
                        StrQuery = StrQuery + ") ";
                        if (i < dgv.Rows.Count - 1)
                            StrQuery = StrQuery + ", ";
                    }
                    StrQuery = StrQuery + ";";
                    Console.Out.WriteLine(StrQuery);
                    using (SqlCommand comm = new SqlCommand(StrQuery,conn))
                    {
                        comm.ExecuteNonQuery();
                    }
                    table1.Rows.Add("succes", "sukses");
                }
                catch (Exception ex)
                {
                    table1.Rows.Add("succes", ex.Message);
                }
                finally
                {
                    sqlcon.Close();
                }
            }
            return table1;
        }

        public static DataTable simplemssql(String perintah)
        {
            DataTable table1 = new DataTable("SimpleMsSql");
            table1.Columns.Add("status");
            table1.Columns.Add("affectedrows");
            using (SqlConnection conn = new SqlConnection("Server=" + parameter.Server + "," + parameter.Port + ";Database=" + parameter.Db + ";User Id=" + parameter.Usern + ";Password=" + parameter.Pass + ";"))
            {
                try
                {
                    SqlCommand command = new SqlCommand(perintah, sqlcon);
                    sqlcon.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    table1.Rows.Add("succes", rowsAffected);
                }

                catch (SqlException exx)
                {
                    table1.Rows.Add("failure", exx.Message);
                }
                finally
                {
                    sqlcon.Close();
                }
            }
            return table1;
        }

        public static DataSet dgsql(String perintah)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataReader dr = null;
            Boolean err = false;
            String errMsg="";
            //using (SqlConnection conn = new SqlConnection("Server=" + server + ";Database=" + db + ";User Id=" + usern + ";Password=" + pass + ";"))
            using (SqlConnection conn = new SqlConnection("Server=" + parameter.Server + ","+ parameter.Port + ";Database=" + parameter.Db + ";User Id=" + parameter.Usern + ";Password=" + parameter.Pass + ";"))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(perintah, conn))
                    {

                        dr = cmd.ExecuteReader();
                        int i = 0;
                        while (!dr.IsClosed)
                        {
                            dt = new DataTable();
                            dt.Load(dr);
                            ds.Tables.Add(dt);
                            if (!dr.HasRows)
                            {
                                break;
                            }
                        }

                    }
                }
                catch (SqlException ex) // This will catch all SQL exceptions
                {
                    Console.WriteLine("SQL exception issue: " + ex.Message);
                    errMsg = "SQL exception issue: " + ex.Message;
                    err = true;
                }
                catch (InvalidOperationException ex) // This will catch SqlConnection Exception
                {
                    Console.WriteLine("Connection Exception issue: " + ex.Message);
                    if (!ex.Message.Contains("HasRows when reader is closed"))
                    {
                        errMsg = "Connection Exception issue: " + ex.Message;
                        err = true;
                    }
                }
                catch (Exception ex) // This will catch every Exception
                {
                    //Will catch all Exception and write the message of the Exception but I do not recommend this to use.
                    Console.WriteLine("Exception Message: " + ex.Message); 
                    errMsg = "Exception Message: " + ex.Message;
                    err = true;
                }
                if (err)
                {
                    DataTable dtr = new DataTable();
                    dtr.Columns.Add("ERROR");
                    dtr.Rows.Add(errMsg);
                    ds.Tables.Add(dtr);
                }
                conn.Close();
            }
            return ds;
        }

        public static async Task<DataSet> AsyncDsSQL(String sql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataReader dr = null;
            Boolean err = false;
            String errMsg = "";
            using (SqlConnection conn = new SqlConnection("Server=" + parameter.Server + "," + parameter.Port + ";Database=" + parameter.Db + ";User Id=" + parameter.Usern + ";Password=" + parameter.Pass + ";"))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        await cmd.Connection.OpenAsync();
                        using (dr = await cmd.ExecuteReaderAsync())
                        {
                            while (true)
                            {
                                dt = new DataTable();
                                dt.Load(dr);
                                ds.Tables.Add(dt);
                                if (!dr.HasRows)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (SqlException ex) // This will catch all SQL exceptions
                    {
                        Console.WriteLine("SQL exception issue: " + ex.Message);
                        errMsg = "SQL exception issue: " + ex.Message;
                        err = true;
                    }
                    catch (InvalidOperationException ex) // This will catch SqlConnection Exception
                    {
                        Console.WriteLine("Connection Exception issue: " + ex.Message);
                        if (!ex.Message.Contains("HasRows when reader is closed"))
                        {
                            errMsg = "Connection Exception issue: " + ex.Message;
                            err = true;
                        }
                    }
                    catch (Exception ex) // This will catch every Exception
                    {
                        //Will catch all Exception and write the message of the Exception but I do not recommend this to use.
                        Console.WriteLine("Exception Message: " + ex.Message);
                        errMsg = "Exception Message: " + ex.Message;
                        err = true;
                    }
                    if (err)
                    {
                        DataTable dtr = new DataTable();
                        dtr.Columns.Add("ERROR");
                        dtr.Rows.Add(errMsg);
                        ds.Tables.Add(dtr);
                    }
                }
            }
            return ds;
        }
        
        public static async void tescon(String servername, String dbname, String username, String password, String port)
        {
            using (SqlConnection conn = new SqlConnection("Server=" + servername + ","+port+";Database=" + dbname + ";User Id=" + username + ";Password=" + password + ";"))
            {
                using (SqlCommand cmd = new SqlCommand("select 1 as hasil", conn))
                {
                    try
                    {
                        await cmd.Connection.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();                        
                        notification.Info("Connection setup", "Successfully connected to DB.");
                    }
                    catch (Exception ex)
                    {
                        notification.Error("Connection setup", "Connect to DB Failed "+ ex.Message.ToString());
                    }
                }
            }
        }

        public static async void insertLog(String id,String modul,String pesan,String ip,String hostname,int sessi)
        {
            using (SqlConnection conn =  new SqlConnection("Server=" + parameter.Server + "," + parameter.Port + ";Database=" + parameter.Db + ";User Id=" + parameter.Usern + ";Password=" + parameter.Pass + ";"))
            {
                String sql = "EXEC insertLog @ident = '" + id + "',@modul='" + modul + "',@messg='" + Helper.AddSlash(pesan) + "',@ip='" + ip + "',@hostn='" + hostname + "',@sessi=" + sessi;
                Console.WriteLine(sql);
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {                        
                        await cmd.Connection.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        //notification.Info("Connection setup", "Successfully connected to DB.");
                    }
                    catch (Exception ex)
                    {
                        notification.Error("Connection setup", "Connect to DB Failed " + ex.Message.ToString());
                    }
                }
            }
        }

        /*++++++++++++++++++++++++++++++++++++ ASYNC ++++++++++++++++++++++++++++++++++++++++++++++++++++=*/
        public static Task<DataSet> TaskDsSQL(String sql)
        {
            return Task.Run(() =>
            {
                DataSet ds = MsSQL.dgsql(sql);
                return ds;
            });
        }

        public static Task<DataTable> TaskSimpleSQL(String sql)
        {
            return Task.Run(() =>
            {
                DataTable dt = MsSQL.simplemssql(sql);
                return dt;
            });
        }

        public static Task<DataSet> TaskinsertAllDatagrid(DataGridView dgv, string kuery, string[] namaKolom, string kueriHapus)
        {
            return Task.Run(() =>
            {
                DataSet ds = new DataSet();
                if (kueriHapus.Length > 2)
                {
                    ds.Tables.Add(MsSQL.simplemssql(kueriHapus));
                }
                ds.Tables.Add(MsSQL.insertAllDatagrid(dgv, kuery, namaKolom));
                return ds;
            });
        }

        public static Task<String> TaskInsertMultiple(String kueryAwal, String data, String pemisah)
        {
            return Task.Run(() =>
            {
                String ds = MsSQL.insertMultipleAct(kueryAwal, data, pemisah);
                return ds;
            });
        }

        public static Task<DataTable> TaskQueryTransaction(String sql, String separator)
        {
            return Task.Run(() =>
            {
                DataTable dt = MsSQL.queryTransaction(sql, separator);
                return dt;
            });
        }

        public static async void insertAllDatagridAsync(DataGridView dgv, string kuery, string[] namaKolom, string kueriHapus = "")
        {
            Preloaderani.addSpinnLoad(dgv);
            DataSet ds = await TaskinsertAllDatagrid(dgv, kuery, namaKolom, kueriHapus);
            Preloaderani.remSpinnLoad(dgv);
        }

        public static async void AsyncDgv(DataGridView dgv, String sql)
        {
            Preloaderani.addSpinnLoad(dgv);
            DataSet ds = await TaskDsSQL(sql);
            DataTable dGridTable = ds.Tables[0];
            dgv.DataSource = dGridTable;
            Preloaderani.remSpinnLoad(dgv);
        }

        /*+++++++++++++++++++++++++++++++++ LOCAL FUNCTION +++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*private static int cek()
        {
            int xx = 1;
            for (int x = 1; x < 5; x++)
            {
                if (mm2[x] == 0)
                {
                    xx = x;
                    mm2[x] = 1;
                    break;
                }
                //System.Diagnostics.Debug.WriteLine(x + " x " + xx);
            }
            return xx;
        }*/
    }
}
