using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Locations
    {
        public DataTable getLocationInfoByCityId(int cityId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationInfoByCityId", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@CityId", SqlDbType.Int).Value = cityId;

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

        public DataTable locationInfoOnlyId(int cityId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationInfoOnlyId", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@CityId", SqlDbType.Int).Value = cityId;

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

        public DataTable countriesList()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_countries", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable statesList(int countryId)
        {
            DataTable dt = new DataTable();

            if (countryId != 0)
            {
                DataSet ds = new DataSet();
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("sp_locationStates", sqlConn);

                try
                {
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.Add("@CountryId", SqlDbType.SmallInt).Value = countryId;
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
            }

            return dt;
        }

        public DataTable citiesList(int stateId)
        {
            DataTable dt = new DataTable();

            if (stateId != 0)
            {
                DataSet ds = new DataSet();
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("sp_locationCities", sqlConn);

                try
                {
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.Add("@StateId", SqlDbType.SmallInt).Value = stateId;
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
            }

            return dt;
        }
    }
}