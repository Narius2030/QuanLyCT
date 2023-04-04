﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLConTy_Entity
{
    internal class DBConnection
    {
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        public void ThucThi(string sqlStr)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Thuc thi that bai\n" + exc.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public DataTable FormLoad(string sqlStr)
        {
            DataTable dataSet = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                adapter.Fill(dataSet);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
            return dataSet;
        }
        public int TienDoDuAn(string MaDA)
        {
            int result = new int();
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT SUM(TienDo/5) FROM PHANCONGDUAN WHERE MaDA = '{MaDA}' GROUP BY MaDA", conn);
                result = (int)command.ExecuteScalar();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                conn.Close();
            }
            if (result >= 100)
            {
                return 100;
            }
            else
            {
                return result;
            }
        }
    }
}