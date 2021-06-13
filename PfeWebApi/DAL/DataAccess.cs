using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PfeWebApi.DAL
{
    public class DataAccess
    {
        public static string str = ConfigurationManager.ConnectionStrings["PfeDB"].ConnectionString;

        public static void setData(SqlCommand cmd,out string message)
        {
            try
            {
                SqlConnection con = new SqlConnection(str);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                message = "ok";
            }
            catch (Exception ex)
            {
               message = ex.Message;
            }

        }
        public static DataTable getData(SqlCommand cmd,out string message)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(str);
                cmd.Connection = con;
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();
                message = "ok";
                return dt;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }
    }
}