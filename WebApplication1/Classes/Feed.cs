using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Feed
    {
        public DataTable eventsFollowing(int userId)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_feedFollowingEvents", sqlConn);

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
                DataTable dtEvents = new DataTable();
                DataRow drEvents = null;

                //define the columns
                dtEvents.Columns.Add(new DataColumn("EventId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("Name", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("Date", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("TypeId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("CoverId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("UserId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("UserPhotoUrl", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("UserFullName", typeof(string)));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    drEvents = dtEvents.NewRow();

                    //add values to each rows
                    drEvents["EventId"] = dt.Rows[i]["EventId"].ToString();
                    drEvents["Name"] = dt.Rows[i]["Name"].ToString();
                    drEvents["Date"] = dt.Rows[i]["Date"].ToString();
                    drEvents["TypeId"] = dt.Rows[i]["TypeId"].ToString();
                    drEvents["CoverId"] = dt.Rows[i]["CoverId"].ToString();
                    drEvents["UserId"] = dt.Rows[i]["UserId"].ToString();
                    drEvents["UserFullName"] = dt.Rows[i]["FirstName"].ToString() + dt.Rows[i]["LastName"].ToString();

                    string profilePicUrl;
                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-220.jpg";
                    }
                    else
                    {
                        profilePicUrl = "~/Images/ProfilesPhotos/nophoto220.png";
                    }
                    drEvents["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dtEvents.Rows.Add(drEvents);
                }
                return dtEvents;
            }
        }

        public DataTable eventsCity(int locationId)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_feedCityAvailableEvents", sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;

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
                DataTable dtEvents = new DataTable();
                DataRow drEvents = null;

                //define the columns
                dtEvents.Columns.Add(new DataColumn("EventId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("Name", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("Date", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("TypeId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("CoverId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("UserId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("UserPhotoUrl", typeof(string)));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    drEvents = dtEvents.NewRow();

                    //add values to each rows
                    drEvents["EventId"] = dt.Rows[i]["EventId"].ToString();
                    drEvents["Name"] = dt.Rows[i]["Name"].ToString();
                    drEvents["Date"] = dt.Rows[i]["Date"].ToString();
                    drEvents["TypeId"] = dt.Rows[i]["TypeId"].ToString();
                    drEvents["CoverId"] = dt.Rows[i]["CoverId"].ToString();
                    drEvents["UserId"] = dt.Rows[i]["UserId"].ToString();

                    string profilePicUrl;
                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-220.jpg";
                    }
                    else
                    {
                        profilePicUrl = "~/Images/ProfilesPhotos/nophoto220.png";
                    }
                    drEvents["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dtEvents.Rows.Add(drEvents);
                }
                return dtEvents;
            }
        }
    }
}