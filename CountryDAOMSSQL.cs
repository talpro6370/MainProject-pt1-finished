using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.DAO
{
    public class CountryDAOMSSQL:ICountryDAO
    {        
        public CountryDAOMSSQL()
        {
            
        }
        public void Add( Country country)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddIntoCountry", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CountryName", country.COUNTRY_NAME));
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        public Country Get(int id)
        {
            Country country = new Country();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCountryById", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@countryId", id));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read()==true)
                {
                    country.ID = (long)reader["ID"];
                    country.COUNTRY_NAME = (string)reader["COUNTRY_NAME"];
                }
                cmd.Connection.Close();
                return country;
            }
        }
        public List<Country> GetAll()
        {
            Country country;
            List<Country> listOfCountries = new List<Country>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllCountries", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    country = new Country
                    {
                        ID = (long)reader["ID"],
                        COUNTRY_NAME = (string)reader["COUNTRY_NAME"]
                    };
                    listOfCountries.Add(country);
                }
                cmd.Connection.Close();
            }
            return listOfCountries;
        }
        public void Remove(Country country)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("RemoveCountry", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", country.ID));
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        public void Update(Country country)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateCountry", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", country.ID));
                cmd.Parameters.Add(new SqlParameter("@countryName", country.COUNTRY_NAME));
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        public long GetCountryId( string countryName)
        {
            long id = 0;
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCountryIdByName", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@countryName", countryName));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    id = (long)reader["ID"];
                }
                cmd.Connection.Close();
            }
            
                return id;
        }


    }
}
