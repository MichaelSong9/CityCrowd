using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class SiteSettings
    {
        public string getSettings(string item)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settings", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            switch (item.ToLower())
            {
                case "loginallow":
                    {
                        return dt.Rows[0]["LoginAllow"].ToString();
                        break;
                    }
                case "registerallow":
                    {
                        return dt.Rows[0]["RegisterAllow"].ToString();
                        break;
                    }
                case "status":
                    {
                        return dt.Rows[0]["Status"].ToString();
                        break;
                    }
                default:
                    {
                        return "0";
                        break;
                    }
            }
            
        }
    }
}