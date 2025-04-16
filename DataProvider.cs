using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace ProjectStudentTuitionManagement
{
    internal class DataProvider
    {
        public SqlConnection cnn;
        public SqlCommand cmd;
        public DataTable dta;
        public SqlDataAdapter ada;
        private string strKN = "Data Source=DESKTOP\\HAYLAMDMM;Initial Catalog=NHOM7_LTUD;Integrated Security=True";


        public void KetNoiDl()
        {
            cnn = new SqlConnection(strKN);
            cnn.Open();
        }
        public void HuyKN()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        public void Doc_DL(string sql, Action<SqlDataReader> xuLyDuLieu)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strKN))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        xuLyDuLieu(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc dữ liệu: " + ex.Message);
            }
        }

        public void ThucThi(string sql)
        {
            try
            {
                KetNoiDl();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực thi truy vấn: " + ex.Message);
            }
            finally
            {
                HuyKN();
            }
        }


        public DataTable Lay_DLbang(string sql)
        {
            KetNoiDl();
            DataTable dta = new DataTable();
            ada = new SqlDataAdapter(sql, cnn);
            ada.Fill(dta);
            return dta;

        }
        public object Lay_GiaTriDon(string query)
        {
            using (SqlConnection conn = new SqlConnection(strKN))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                return result ?? 0; // nếu null thì trả về 0
            }
        }

        public bool ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(strKN))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return false;
                }
            }
        }

        // Hàm gọi stored procedure có trả về dữ liệu (ví dụ: SELECT)
        public DataTable ExecuteQuery(string storedProcedureName, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(strKN))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            return dt;
        }
    }
}

