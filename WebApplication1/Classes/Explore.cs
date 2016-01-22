using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Explore
    {
        public Tuple<int, DataTable> startRecommending(int userId)
        {
            DataTable dt = new DataTable();
            int status = 0;

            Int64 eventId = getRecommendation(userId);

            if (eventId != 0)
            {
                dt = eventInfo(eventId, userId);
            }
            else
            {
                if (createRecommendations(userId))
                {
                    eventId = getRecommendation(userId);


                    if (eventId != 0)
                    {
                        dt = eventInfo(eventId, userId);
                    }
                    else
                    {
                        status = -1;
                    }
                }
                else
                {
                    status = -1;
                }
            }

            return new Tuple<int, DataTable>(status, dt);
        }

        protected DataTable eventInfo(Int64 eventId, int userId)
        {
            DataTable dt = new DataTable();
            Classes.Events e = new Classes.Events();
            return e.eventInfo(eventId, userId);
        }

        protected Int64 getRecommendation(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventRecommendationGet", sqlConn);
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

            if (dt.Rows.Count != 0)
            {
                return Convert.ToInt64(dt.Rows[0]["EventId"].ToString());
            }
            else
            {
                return 0;
            }
        }

        protected bool createRecommendations(int userId)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventRecommend", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            try
            {
                sqlConn.Open();
                sda.Fill(ds);
                dt1 = ds.Tables[0];
                dt2 = ds.Tables[1];
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

            int lastEventId = 0;

            if (dt1.Rows.Count == 0)
            {
                lastEventId = Convert.ToInt32(dt2.Rows[0]["EventId"].ToString());
                setLastRecommendation(userId, lastEventId);

                return false;
            }
            else
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    lastEventId = Convert.ToInt32(dt1.Rows[i]["EventId"].ToString());
                    SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlDataAdapter sda2 = new SqlDataAdapter("sp_eventRecommendCheck", sqlConn2);
                    sda2.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda2.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    sda2.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = lastEventId;
                    DataTable dt3 = new DataTable();
                    DataSet ds2 = new DataSet();

                    try
                    {
                        sqlConn2.Open();
                        sda2.Fill(ds2);
                        dt3 = ds2.Tables[0];
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlConn2.Close();
                        sda2.Dispose();
                        sqlConn2.Dispose();
                    }

                    if (dt3.Rows.Count == 0)
                    {

                        SqlCommand sqlCmd = new SqlCommand("sp_eventRecommendationCreate", sqlConn);
                        SqlConnection sqlConn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = lastEventId;
                        sqlCmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = dt1.Rows[i]["Date"].ToString();

                        try
                        {
                        
                            sqlConn3.Open();
                            sqlCmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            sqlConn3.Close();
                            sqlCmd.Dispose();
                            sqlConn3.Dispose();
                        }
                    }
                }

                setLastRecommendation(userId, lastEventId);

                return true;
            }
        }

        protected void setLastRecommendation(int userId, int eventId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_lastRecommendationSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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