using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Notifications
    {
        public void allRead(int userId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_notificationsAllRead", sqlConn);

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }
        }

        public DataTable notifications(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_notifications", sqlConn2);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.Fill(ds);
            dt = ds.Tables[0];
            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("NotificationId", typeof(string)));
            dt2.Columns.Add(new DataColumn("NotificationType", typeof(string)));
            dt2.Columns.Add(new DataColumn("Text", typeof(string)));
            dt2.Columns.Add(new DataColumn("Unread", typeof(string)));
            dt2.Columns.Add(new DataColumn("SubmitDate", typeof(string)));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["NotificationId"] = dt.Rows[i]["NotificationId"].ToString();
                dr2["NotificationType"] = dt.Rows[i]["NotificationType"].ToString();
                dr2["Text"] = dt.Rows[i]["Text"].ToString();
                dr2["Unread"] = dt.Rows[i]["Unread"].ToString();
                dr2["SubmitDate"] = dt.Rows[i]["SubmitDate"].ToString();

                //add the row to DataTable
                dt2.Rows.Add(dr2);
            }

            return dt2;
        }
        
        public void notificationAdd(int UserId, int NotificationType, string Value1, string Value2)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_notificationAdd", sqlConn);

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                sqlCmd.Parameters.Add("@NotificationType", SqlDbType.Int).Value = NotificationType;
                sqlCmd.Parameters.Add("@Text", SqlDbType.NVarChar).Value = notificationText(NotificationType, Value1, Value2);

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }
        }

        public string notificationText(int NotificationType, string Value1, string Value2)
        {
            switch (NotificationType)
            {
                case 1:
                    {
                        return "تبریک می گوییم! شما به عضویت سایت درآمدید!";
                        break;
                    }
                case 2:
                    {
                        return "با درخواست ایجاد پروفایل شما با نام <strong>" + Value1 + "</strong> موافقت شد. (<a class='NormalLink' href='Profile/" + Value2 + "'>ویرایش پروفایل</a>)";
                        break;
                    }
                case 3:
                    {
                        return "با درخواست ایجاد پروفایل شما با نام <strong>" + Value1 + "</strong> مخالفت شد. (دلیل رد درخواست: " + Value2 + ")";
                        break;
                    }
                case 4:
                    {
                        return "<a class='NormalLink' href='Profile/" + Value2 + "'><strong>" + Value1 + "</strong></a> با درخواست شما موافقت نمود و به فهرست دوستان شما اضافه گردید.";
                        break;
                    }
                default:
                    {
                        return "";
                        break;
                    }
            }
        }

        public void notificationDelete(int UserId, int NotificationId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_notificationDelete", sqlConn);

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@NotificationId", SqlDbType.BigInt).Value = NotificationId;

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }
        }
    }
}