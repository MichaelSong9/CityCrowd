using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApplication1.Classes
{
    public class UserProfileSet
    {
        public Int16 completion(int userId, string username, string firstName, string lastName, Int16 gender, int locationId, string birthDate)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_completion", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
            sqlCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstName;
            sqlCmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
            sqlCmd.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = gender;
            sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;
            //DOB
            if (birthDate == "0")
            {
                birthDate = "";
            }
            sqlCmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = 1985;//Convert.ToDateTime(dob);

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

            return 1;
        }

        public Tuple<int, string, int> register(string email, string password1, string password2)
        {
            int status = 0;
            string message = "";
            int userId = 0;

            if (password1.Length == 0) // check if email entered blank
            {
                status = -1;
                message = "Enter email address!";
            }
            else // check if email is correct
            {
                string expression = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

                Match match = Regex.Match(email, expression, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    status = -1;
                    message = "Email address is not correct!";
                }
                else
                {
                    if (password1.Length == 0) // check if password is blank
                    {
                        status = -1;
                        message = "Enter password!";
                    }
                    else
                    {
                        if (password1.Length < 4) // check if password is less than 4 characters
                        {
                            status = -1;
                            message = "Password must be at least 4 characters!";
                        }
                        else
                        {
                            if (password1 != password2) // check if password and retype password are the same
                            {
                                status = -1;
                                message = "Password and retype password must be the same!";
                            }
                            else
                            {
                                Classes.UserInfo ui = new Classes.UserInfo();
                                int UserId = ui.getUserIdByEmail(email);
                                if (UserId != 0) //user registered before
                                {
                                    status = -1;
                                    message = "Email address already registered before!";
                                }
                                else
                                {
                                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                    SqlCommand sqlCmd = new SqlCommand("sp_register", sqlConn);
                                    sqlCmd.CommandType = CommandType.StoredProcedure;
                                    sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                                    sqlCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password1;
                                    sqlCmd.Parameters.Add("@VCode", SqlDbType.NVarChar).Value = Convert.ToString(Guid.NewGuid());

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
                                }
                            }
                        }
                    }
                }
            }

            return new Tuple<int, string, int>(status, message, userId);
        }
    }


}