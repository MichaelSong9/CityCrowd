using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Events
    {
        public DataTable eventslist(int userId, string mode)
        {
            string spName = "";

            switch (mode)
            {
                case "created":
                    spName = "sp_eventsCreated";
                    break;
                case "accepted":
                    spName = "sp_eventsAccepted";
                    break;
                case "requested":
                    spName = "sp_eventsRequested";
                    break;
            }

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(spName, sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.Fill(ds);
            dt = ds.Tables[0];
            DataTable dtEvents = new DataTable();
            DataRow drEvents = null;

            //define the columns
            dtEvents.Columns.Add(new DataColumn("EventId", typeof(string)));
            dtEvents.Columns.Add(new DataColumn("Name", typeof(string)));
            dtEvents.Columns.Add(new DataColumn("Date", typeof(string)));
            dtEvents.Columns.Add(new DataColumn("TypeId", typeof(string)));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //create new row
                drEvents = dtEvents.NewRow();

                //add values to each rows
                drEvents["EventId"] = dt.Rows[i]["EventId"].ToString();
                drEvents["Name"] = dt.Rows[i]["Name"].ToString();
                drEvents["Date"] = dt.Rows[i]["Date"].ToString();
                drEvents["TypeId"] = dt.Rows[i]["TypeId"].ToString();

                //add the row to DataTable
                dtEvents.Rows.Add(drEvents);
            }

            return dt;
        }

        public int eventAdd(int userId, string name, string date, int participants, int locationId, Int16 privacy, Int16 typeId, int coverId, string languages, string descriptions, bool hasPhoto, bool openEvent)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_eventAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // user id
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            // name +max 50
            sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            // date +add time
            sqlCmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = date;
            // participants +limit
            sqlCmd.Parameters.Add("@Participants", SqlDbType.Int).Value = participants;
            // location +near own location or trip
            sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;
            // type +1 or 2
            sqlCmd.Parameters.Add("@Privacy", SqlDbType.SmallInt).Value = privacy;
            // type id +limit
            sqlCmd.Parameters.Add("@TypeId", SqlDbType.SmallInt).Value = typeId;
            // type id +limit
            sqlCmd.Parameters.Add("@CoverId", SqlDbType.SmallInt).Value = coverId;
            // languages +validate
            sqlCmd.Parameters.Add("@Languages", SqlDbType.VarChar).Value = languages;
            // descriptions +limit to max
            sqlCmd.Parameters.Add("@Descriptions", SqlDbType.NVarChar).Value = descriptions;
            // event photo
            sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = hasPhoto;
            // event photo
            sqlCmd.Parameters.Add("@OpenEvent", SqlDbType.Bit).Value = openEvent;

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
                sqlCmd.Dispose();
            }

            return 1;
        }

        public int eventModify(int userId, int eventId, string name, string date, int participants, int locationId, Int16 privacy, Int16 typeId, int coverId, string languages, string descriptions, bool hasPhoto, bool openEvent)
        {

            return 1;
        }

        public int eventDelete(int eventId)
        {

            return 1;
        }

        public DataTable eventBoardMessages(Int64 eventId, int ownerId)
        {
            DataTable dt = new DataTable();
            DataTable dtCountries = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventBoardMessages", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
                dtCountries = ds.Tables[1];
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

            if (dt.Rows.Count == 0)// no messages
            {
                return dt;
            }
            else
            {

                DataTable dt2 = new DataTable();
                DataRow dr2 = null;

                //define the columns
                dt2.Columns.Add(new DataColumn("MessageId", typeof(string)));
                dt2.Columns.Add(new DataColumn("IsOwner", typeof(string)));
                dt2.Columns.Add(new DataColumn("SenderName", typeof(string)));
                dt2.Columns.Add(new DataColumn("SenderId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Message", typeof(string)));
                dt2.Columns.Add(new DataColumn("PassedDate", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["MessageId"] = dt.Rows[i]["MessageId"].ToString();
                    dr2["SenderName"] = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["LastName"].ToString();
                    dr2["SenderId"] = dt.Rows[i]["UserId"].ToString();
                    dr2["Message"] = dt.Rows[i]["Message"].ToString();
                    Classes.Date d = new Classes.Date();
                    dr2["PassedDate"] = d.FormatPassedDate(dt.Rows[i]["SubmitDate"].ToString());

                    if (ownerId == Convert.ToInt32(dt.Rows[i]["UserId"].ToString()))
                    {
                        dr2["IsOwner"] = "true";
                    }
                    else
                    {
                        dr2["IsOwner"] = "false";
                    }

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
        }

        public DataTable eventParticipants(Int64 eventId)
        {
            DataTable dtParticipants = new DataTable();
            DataSet dsParticipants = new DataSet();
            SqlConnection sqlConnParticipants = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sdaParticipants = new SqlDataAdapter("sp_eventParticipants", sqlConnParticipants);

            sdaParticipants.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaParticipants.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            try
            {
                sdaParticipants.Fill(dsParticipants);
                dtParticipants = dsParticipants.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnParticipants.Close();
                sdaParticipants.Dispose();
                sqlConnParticipants.Dispose();
            }

            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
            dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
            dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
            dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

            string profilePicUrl;

            for (int i = 0; i < dtParticipants.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["UserId"] = dtParticipants.Rows[i]["UserId"].ToString();
                dr2["FirstName"] = dtParticipants.Rows[i]["FirstName"].ToString();
                dr2["LastName"] = dtParticipants.Rows[i]["LastName"].ToString();

                // Show profile's photo
                if (Convert.ToBoolean(dtParticipants.Rows[i]["HasPhoto"].ToString()))
                {
                    profilePicUrl = "~/Files/ProfilesPhotos/" + dtParticipants.Rows[i]["UserId"].ToString() + "-220.jpg";
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

        public int eventBookmark(int userId, Int64 eventId)
        {
            int status = 0;

            DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("sp_bookmarkEventCheckExists", sqlConn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
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
                    sda.Dispose();
                }

                if (dt.Rows.Count == 0)
                {
                    SqlCommand sqlCmd = new SqlCommand("sp_bookmarkEventAdd", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

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
                        sqlCmd.Dispose();
                    }

                    status = 1;
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("sp_bookmarkEventDelete", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

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
                        sqlCmd.Dispose();
                        sqlConn.Dispose();
                    }

                    status = 2;
                }

            return status;
        }

        public int eventDelete(Int64 eventId, int userId)
        {
            int status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_eventDelete", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
                sqlCmd.Dispose();
            }
            status = 1;

            return status;
        }

        public Tuple<int, string> eventModify(Int64 eventId, string name, string date, string participants, int locationId, int privacy, int typeId, int coverId, string languages, string descriptions, bool openEvent)
        {
            int status = 1;
            string message = "";

            if (name.Length == 0) // check name
            {
                status = -1;
                message = "Enter name<br/>";
            }
            if (date.Length == 0) // check date
            {
                status = -1;
                message += "Enter date<br/>";
            }
            //if (TextBoxParticipants.Text.Length == 0) // check participants +++only number
            //{
            //    status = -1;
            //    message += "Enter participants number<br/>";
            //}
            if (languages.Length < 2) // check language
            {
                status = -1;
                message += "Enter language<br/>";
            }
            // check duration
            double num;
            string candidate = participants;
            if (!double.TryParse(candidate, out num) && participants.Length != 0)
            {
                status = -1;
                message += "participants number is not valid, enter number<br/>";
            }

            if (status != -1) // there is something wrong with the values that user entered
            {// user entered valid values
                // add to the database
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_eventModifySet", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                // event id
                sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = Convert.ToInt32(eventId);
                // name +max 50
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                // date +add time
                sqlCmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = date;
                // participants +limit
                sqlCmd.Parameters.Add("@Participants", SqlDbType.Int).Value = participants;
                // location +near own location or trip
                sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;
                // city +get from front end
                sqlCmd.Parameters.Add("@City", SqlDbType.VarChar).Value = "1"; ////////////////
                // type +1 or 2
                sqlCmd.Parameters.Add("@Privacy", SqlDbType.SmallInt).Value = privacy;
                // mood id +limit
                sqlCmd.Parameters.Add("@TypeId", SqlDbType.SmallInt).Value = typeId;
                // languages +validate
                sqlCmd.Parameters.Add("@Languages", SqlDbType.VarChar).Value = languages;
                // descriptions +limit to max
                sqlCmd.Parameters.Add("@Descriptions", SqlDbType.NVarChar).Value = descriptions;
                // repeat type +limit
                sqlCmd.Parameters.Add("@OpenEvent", SqlDbType.Bit).Value = false; /////////////////////////

                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                }
                catch
                {

                }
                finally
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlCmd.Dispose();
                }
            }

            return new Tuple<int, string>(status, message);
        }

        public DataTable eventModifyInfo(Int64 eventId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventModifyGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            return dt;
        }

        public DataTable eventInfo(Int64 eventId, int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventInfoGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            int OwnerId = 0;

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

            return dt;
        }

    }
}