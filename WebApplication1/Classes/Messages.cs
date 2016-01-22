using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Messages
    {
        public void allRead(int userId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_messagesAllRead", sqlConn);

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

        public DataTable messageLists(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_messageLists", sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            try
            {
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

            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("MessageListId", typeof(string)));
            dt2.Columns.Add(new DataColumn("OtherId", typeof(string)));
            dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
            dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
            dt2.Columns.Add(new DataColumn("PassedDate", typeof(string)));
            dt2.Columns.Add(new DataColumn("Brief", typeof(string)));
            dt2.Columns.Add(new DataColumn("Unread", typeof(string)));
            dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
            dt2.Columns.Add(new DataColumn("NewCount", typeof(string)));

            string profilePicUrl;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["MessageListId"] = dt.Rows[i]["MessageListId"].ToString();
                dr2["OtherId"] = dt.Rows[i]["OtherId"].ToString();
                dr2["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                dr2["LastName"] = dt.Rows[i]["LastName"].ToString();
                dr2["Brief"] = dt.Rows[i]["Brief"].ToString();
                dr2["Unread"] = dt.Rows[i]["Unread"].ToString();
                dr2["NewCount"] = dt.Rows[i]["NewCount"].ToString();
                Classes.Date d = new Classes.Date();
                dr2["PassedDate"] = d.FormatPassedDate(dt.Rows[i]["SubmitDate"].ToString());

                // Show profile's photo
                if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                {
                    profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-220.jpg";
                }
                else
                {
                    profilePicUrl = "~/Images/ProfilesPhotos/nophoto220.png";
                }
                dr2["ProfilePicUrl"] = profilePicUrl;

                //add the row to DataTable
                dt2.Rows.Add(dr2);
            }
            return dt2;
        }

        public Tuple<int, DataTable, DataTable, DataTable> showMessages(int userId, int otherId)
        {
            int status = 0;
            DataTable dtUserName = new DataTable();
            DataTable dtOtherName = new DataTable();
            DataTable dtMessages = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_messagesSenderReceiverInfo", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@OtherId", SqlDbType.Int).Value = otherId;

            //try
            //{
                sda.Fill(ds);
                dtUserName = ds.Tables[0];
                dtOtherName = ds.Tables[1];

                if (dtUserName.Rows.Count == 0 || dtOtherName.Rows.Count == 0)
                {
                    status = -1;
                }
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    sqlConn.Close();
            //    sda.Dispose();
            //    sqlConn.Dispose();
            //}

            if (status != -1)
            {
                DataTable dt1 = new DataTable();
                DataSet ds1 = new DataSet();
                SqlConnection sqlConn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlDataAdapter sda1 = new SqlDataAdapter("sp_messages", sqlConn3);

                sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda1.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
                sda1.SelectCommand.Parameters.Add("@OtherId", SqlDbType.Int).Value = otherId;
                sda1.Fill(ds1);
                dt1 = ds1.Tables[0];
                DataRow dr2 = null;

                //define the columns
                dtMessages.Columns.Add(new DataColumn("MessageId", typeof(string)));
                dtMessages.Columns.Add(new DataColumn("Sender", typeof(string)));
                dtMessages.Columns.Add(new DataColumn("Message", typeof(string)));
                dtMessages.Columns.Add(new DataColumn("PassedDate", typeof(string)));
                dtMessages.Columns.Add(new DataColumn("Unread", typeof(string)));
                dtMessages.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dtMessages.NewRow();

                    //add values to each rows
                    dr2["MessageId"] = dt1.Rows[i]["MessageId"].ToString();
                    dr2["Sender"] = dt1.Rows[i]["Sender"].ToString();
                    dr2["Message"] = dt1.Rows[i]["Message"].ToString();
                    dr2["Unread"] = dt1.Rows[i]["Unread"].ToString();
                    Classes.Date d = new Classes.Date();
                    dr2["PassedDate"] = d.FormatPassedDate(dt1.Rows[i]["SubmitDate"].ToString());

                    // Show profile's photo
                    //if (Convert.ToBoolean(dt1.Rows[i]["HasPhoto"].ToString()))
                    //{
                    //    profilePicUrl = "~/Files/ProfilesPhotos/" + dt1.Rows[i]["UserId"].ToString() + "-220.jpg";
                    //}
                    //else
                    //{
                    profilePicUrl = "~/Images/ProfilesPhotos/nophoto220.png";
                    //}
                    dr2["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dtMessages.Rows.Add(dr2);
                }
            }

            return new Tuple<int, DataTable, DataTable, DataTable>(status, dtUserName, dtOtherName, dtMessages);
        }

        public int addMessage(int userId, int receiverId, string message)
        {
            int status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_messagesSendAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SenderId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@ReceiverId", SqlDbType.Int).Value = receiverId;
            sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;
            if (message.Length < 100)
            {
                sqlCmd.Parameters.Add("@Brief", SqlDbType.NVarChar).Value = message;
            }
            else
            {
                sqlCmd.Parameters.Add("@Brief", SqlDbType.NVarChar).Value = message.Substring(0, 100);
            }

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                status = 0;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }

            return status;
        }
    }
}