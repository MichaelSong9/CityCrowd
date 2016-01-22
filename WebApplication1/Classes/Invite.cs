using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Invite
    {
        public int inviteSend(int userId, string email, string name, string message, int type)
        {
            int status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_invite", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@Type", SqlDbType.SmallInt).Value = type;

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
    }
}