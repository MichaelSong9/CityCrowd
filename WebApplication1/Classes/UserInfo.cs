using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class UserInfo
    {
        public Int16 getUserStatus(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserStatusById", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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

            sda.Dispose();
            sqlConn.Dispose();

            if (dt.Rows.Count == 0)
            {
                return 10; //no user found
            }
            else
            {
                return Convert.ToInt16(dt.Rows[0]["Status"].ToString());
            }
        }

        public Int32 getUserIdByEmail(string Email)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserIdByEmail", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;
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

            sda.Dispose();
            sqlConn.Dispose();

            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
            }
        }

        public string getUserFullNameByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserFullNameByUserId", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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

            sda.Dispose();
            sqlConn.Dispose();

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
            }
        }

        public int locationIdByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserLocationByUserId", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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

            sda.Dispose();
            sqlConn.Dispose();

            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["LocationId"].ToString());
            }
        }

        public DataTable masterPageInfo(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_userInfoMaster", sqlConn);
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
            return dt;
        }

        public DataTable followersList(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_followers", sqlConn);

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
                DataRow dr2 = null;

                //define the columns
                dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
                dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["UserId"] = dt.Rows[i]["FollowerId"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                    dr2["LastName"] = dt.Rows[i]["LastName"].ToString();

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

        public DataTable followingList(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_following", sqlConn);

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
                DataRow dr2 = null;

                //define the columns
                dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
                dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["UserId"] = dt.Rows[i]["UserId"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                    dr2["LastName"] = dt.Rows[i]["LastName"].ToString();

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

            //public Array getMenuInfo(int UserId)
            //{
            //    DataTable dt = new DataTable();
            //    DataSet ds = new DataSet();
            //    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            //    SqlDataAdapter sda = new SqlDataAdapter("sp_getMenuInfo", sqlConn);

            //    try
            //    {
            //        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            //        sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            //        sda.Fill(ds);
            //        dt = ds.Tables[0];

            //        string photoUrl = "Images/NoPhoto.png";
            //        if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
            //        {
            //            photoUrl = "Files/" + ConfigurationManager.AppSettings["folderName"].ToString() + "/ProfilesPhotos/" + UserId.ToString() + ".jpg";
            //        }

            //        string[] values = new string[]{
            //        dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString(),
            //        dt.Rows[0]["Username"].ToString(),
            //        photoUrl};
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //    finally
            //    {
            //        sqlConn.Close();
            //        sda.Dispose();
            //        sqlConn.Dispose();
            //    }

            //    return values[];
            //}
        }

        public bool isUserFollower(int userId, int followerId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_isUserFollower", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sda.SelectCommand.Parameters.Add("@FollowerId", SqlDbType.Int).Value = followerId;
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

            if (dt.Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tuple<int, DataTable> profileInfo(int userId, string profileId)
        {
            int status = 0;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getProfileInfoByUsername", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = profileId;

            //check if query string is user id or username
            double num;
            if (double.TryParse(profileId, out num))
            {
                sda = new SqlDataAdapter("sp_getProfileInfoByUserId", sqlConn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(profileId);
            }

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                status = -1;
            }
            else
            {
                // Profile is redistrected
                if (Convert.ToInt32(dt.Rows[0]["Status"].ToString()) != 1)
                {
                    status = -2;
                }
                else
                {
                    status = 1;
                    // generate the info

                    DataRow dr2 = null;

                    //define the columns
                    dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
                    dt2.Columns.Add(new DataColumn("isOwner", typeof(string)));
                    dt2.Columns.Add(new DataColumn("isFollower", typeof(string)));
                    dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
                    dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                    dt2.Columns.Add(new DataColumn("FollowersCount", typeof(string)));
                    dt2.Columns.Add(new DataColumn("FollowingCount", typeof(string)));
                    dt2.Columns.Add(new DataColumn("About", typeof(string)));
                    dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
                    dt2.Columns.Add(new DataColumn("ProfileVerified", typeof(string)));
                    dt2.Columns.Add(new DataColumn("RateCount", typeof(string)));
                    dt2.Columns.Add(new DataColumn("RatePercent", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CountryId", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CountryName", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CityName", typeof(string)));


                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    if (userId.ToString() == dt.Rows[0]["UserId"].ToString()) //user is the profile owner
                    {
                        dr2["isOwner"] = "true";
                    }
                    else
                    {
                        dr2["isOwner"] = "false";
                    }

                    // is visitor follower
                    bool isFollower = false;
                    if (userId != 0)
                    {
                        isFollower = isUserFollower(Convert.ToInt32(dt.Rows[0]["UserId"].ToString()), userId);
                    }
                    if (isFollower)
                    {
                        dr2["isFollower"] = "true";
                    }
                    else
                    {
                        dr2["isFollower"] = "false";
                    }

                    // Profile info
                    dr2["FirstName"] = dt.Rows[0]["FirstName"].ToString();
                    dr2["LastName"] = dt.Rows[0]["LastName"].ToString();
                    dr2["Username"] = dt.Rows[0]["Username"].ToString();
                    dr2["FollowersCount"] = dt.Rows[0]["FollowersCount"].ToString();
                    dr2["FollowingCount"] = dt.Rows[0]["FollowingCount"].ToString();
                    dr2["About"] = dt.Rows[0]["About"].ToString();
                    dr2["UserId"] = dt.Rows[0]["UserId"].ToString();
                    dr2["ProfileVerified"] = dt.Rows[0]["ProfileVerified"].ToString();
                    dr2["RateCount"] = dt.Rows[0]["RateCount"].ToString();

                    // Rate
                    int rateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());
                    int rateCount = Convert.ToInt32(dt.Rows[0]["rateCount"].ToString());
                    int rateScore = Convert.ToInt32(dt.Rows[0]["rateScore"].ToString());
                    if (rateCount >= rateSufficient)
                    {
                        int popularity = (20 * rateScore) / rateCount;
                        dr2["RatePercent"] = popularity.ToString();
                    }
                    else
                    {
                        dr2["RatePercent"] = "NA";
                    }

                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                    {
                        dr2["ProfilePicUrl"] = "Files/ProfilesPhotos/" + dt.Rows[0]["UserId"].ToString() + "-220.jpg";
                    }
                    else
                    {
                        dr2["ProfilePicUrl"] = "Images/nophoto.png";
                    }

                    //add the row to DataTable
                    dt2.Rows.Add(dr2);
                }
            }

            return new Tuple<int, DataTable>(status, dt2);
        }

        public Tuple<int[], DataTable> reviews(int userId)
        {
            int one = 0;
            int two = 0;
            int three = 0;
            int four = 0;
            int five = 0;

            DataTable dtReviews = new DataTable();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_reviews", sqlConn);

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

            if (dt.Rows.Count > 0)
            {
                DataRow drReviews = null;

                //define the columns
                dtReviews.Columns.Add(new DataColumn("FirstName", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("LastName", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("Username", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("Rate", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("Comment", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("SubmitDate", typeof(string)));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    drReviews = dtReviews.NewRow();

                    //add values to each rows
                    drReviews["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                    drReviews["LastName"] = dt.Rows[i]["LastName"].ToString();
                    drReviews["Username"] = dt.Rows[i]["Username"].ToString();
                    drReviews["Rate"] = dt.Rows[i]["Rate"].ToString();
                    drReviews["Comment"] = dt.Rows[i]["Comment"].ToString();
                    drReviews["SubmitDate"] = dt.Rows[i]["SubmitDate"].ToString();

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
                    drReviews["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dtReviews.Rows.Add(drReviews);

                    int rate = Convert.ToInt16(dt.Rows[i]["Rate"].ToString());
                    switch (rate)
                    {
                        case 1:
                            {
                                one = one + 1;
                                break;
                            }
                        case 2:
                            {
                                two = two + 1;
                                break;
                            }
                        case 3:
                            {
                                three = three + 1;
                                break;
                            }
                        case 4:
                            {
                                four = four + 1;
                                break;
                            }
                        case 5:
                            {
                                five = five + 1;
                                break;
                            }
                    }
                }

                //convert DataTable to DataView
                DataView dv = dtReviews.DefaultView;
                //save our newly ordered results back into our datatable
                dtReviews = dv.ToTable();
            }

            int[] ratesCount = new int[] {one, two, three, four, five};

            return new Tuple<int[], DataTable>(ratesCount, dtReviews);
        }
    }
}