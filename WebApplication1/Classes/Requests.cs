using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Requests
    {
        public void allRead(int userId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_requestsAllRead", sqlConn);

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

        public DataTable requestsList(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventRequests", sqlConn);

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

            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {
                DataTable dt2 = new DataTable();
                DataRow dr = null;

                //define the columns
                dt2.Columns.Add(new DataColumn("RequestId", typeof(string)));
                dt2.Columns.Add(new DataColumn("EventId", typeof(string)));
                dt2.Columns.Add(new DataColumn("EventName", typeof(string)));
                dt2.Columns.Add(new DataColumn("ParticipantsRemained", typeof(string)));
                dt2.Columns.Add(new DataColumn("SenderId", typeof(string)));
                dt2.Columns.Add(new DataColumn("FullName", typeof(string)));
                dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                dt2.Columns.Add(new DataColumn("Rate", typeof(string)));
                dt2.Columns.Add(new DataColumn("Distance", typeof(string)));
                dt2.Columns.Add(new DataColumn("Message", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
                dt2.Columns.Add(new DataColumn("Gender", typeof(string)));
                dt2.Columns.Add(new DataColumn("Age", typeof(string)));
                dt2.Columns.Add(new DataColumn("City", typeof(string)));
                dt2.Columns.Add(new DataColumn("RemainedTime", typeof(string)));

                int rateCount, rateScore, popularity;
                string rate, profilePicUrl;
                int rateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr = dt2.NewRow();

                    //add values to each rows
                    dr["RequestId"] = dt.Rows[i]["RequestId"].ToString();
                    dr["EventId"] = dt.Rows[i]["EventId"].ToString();
                    dr["EventName"] = dt.Rows[i]["Name"].ToString();
                    dr["ParticipantsRemained"] = dt.Rows[i]["ParticipantsRemained"].ToString();
                    dr["SenderId"] = dt.Rows[i]["UserId"].ToString();
                    dr["FullName"] = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["LastName"].ToString();
                    dr["Username"] = dt.Rows[i]["Username"].ToString();
                    dr["Distance"] = "111"; //////////////////////////////////
                    dr["Message"] = dt.Rows[i]["Message"].ToString();
                    dr["Gender"] = dt.Rows[i]["Gender"].ToString();
                    dr["Age"] = "25"; //////////////////////////////////
                    dr["City"] = "Helsinki"; //////////////////////////////////
                    // time
                    Classes.Date d = new Classes.Date();
                    dr["RemainedTime"] = d.FormatRemainedDate(dt.Rows[i]["Date"].ToString());

                    // Rate
                    rateCount = Convert.ToInt32(dt.Rows[i]["rateCount"].ToString());
                    rateScore = Convert.ToInt32(dt.Rows[i]["rateScore"].ToString());
                    if (rateCount >= rateSufficient)
                    {
                        popularity = (20 * rateScore) / rateCount;
                        rate = popularity.ToString();
                    }
                    else
                    {
                        rate = "NA";
                    }
                    dr["Rate"] = rate;

                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-220.jpg";
                    }
                    else
                    {
                        profilePicUrl = "~/Images/ProfilesPhotos/nophoto220.png";
                    }
                    dr["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dt2.Rows.Add(dr);
                }

                return dt2;
            }
            
        }

        public int requestSend(int userId, int eventId, string message)
        {
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventRequestAdd", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;

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
            }


            if (dt.Rows.Count == 0)// request exists
            {

            }
            else
            {
                status = Convert.ToInt16(dt.Rows[0]["RequestStatus"].ToString());
            }

            return status;
        }

        public int requestAccept(int userId, Int64 requestId)
        {
            int status = 0;

            DataTable dt = new DataTable();
                        DataSet ds = new DataSet();
                        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        SqlDataAdapter sda = new SqlDataAdapter("sp_requestInfo", sqlConn);
                        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
                        sda.SelectCommand.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = requestId;

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



                            if (dt.Rows.Count != 0)// request exists
                            {
                                SqlConnection sqlConn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                SqlCommand sqlCmd1 = new SqlCommand("sp_requestAccept", sqlConn1);
                                sqlCmd1.CommandType = CommandType.StoredProcedure;
                                sqlCmd1.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
                                sqlCmd1.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = requestId;
                                sqlCmd1.Parameters.Add("@EventId", SqlDbType.BigInt).Value = Convert.ToInt32(dt.Rows[0]["EventId"].ToString());
                                sqlCmd1.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                                try
                                {
                                    sqlConn1.Open();
                                    sqlCmd1.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {

                                }
                                finally
                                {
                                    sqlConn1.Close();
                                    sqlCmd1.Dispose();
                                    sqlConn1.Dispose();
                                }

                                ////////////////////////// add notification

                                status = 1;
                            }
                            else
                            {
                                status = -1;
                            }

            return status;
        }

        public void requestReject(Int64 requestId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_requestReject", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = requestId;
            try
            {
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