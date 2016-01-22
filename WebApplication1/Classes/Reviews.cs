using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Reviews
    {
        public DataTable reviewRequestInfo(int userId, Int64 reviewRequestId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);

            SqlDataAdapter sda = new SqlDataAdapter("sp_reviewRequestRead", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@ReviewRequestId", SqlDbType.BigInt).Value = reviewRequestId;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

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

        public void reviewRefuse(int userId, Int64 reviewRequestId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_reviewRequestDelete", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@ReviewRequestId", SqlDbType.BigInt).Value = reviewRequestId;
            sqlCmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

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

        public int reviewAdd(int userId, Int64 reviewRequestId, string comment, int rate)
        {
            int status = 0;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);

            SqlDataAdapter sda = new SqlDataAdapter("sp_reviewRequestRead", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@ReviewRequestId", SqlDbType.BigInt).Value = reviewRequestId;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0) //doesn't exist
                {
                    status = -1; //not found
                }
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

            if (status != -1)
            {
                SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_reviewAdd", sqlConn2);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@ReviewRequestId", SqlDbType.BigInt).Value = reviewRequestId;
                sqlCmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = Convert.ToInt64(dt.Rows[0]["EventId"].ToString());
                sqlCmd.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = comment;
                if (rate > 5)
                {
                    rate = 5;
                }
                sqlCmd.Parameters.Add("@Rate", SqlDbType.TinyInt).Value = rate;

                try
                {
                    sqlConn2.Open();
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
            }

            return status;
        }
    }
}