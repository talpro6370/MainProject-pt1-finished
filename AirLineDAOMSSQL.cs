using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using MainProject.DAO;

namespace MainProject
{
    public class AirLineDAOMSSQL:IAirlineDAO
    {
        
        public AirLineDAOMSSQL()
        {

        }
        public void Add(AirlineCompany company)
        {
            if (ExUserNameInCustomers(company.USER_NAME) == true)
            {
                Console.WriteLine("The user name is already exist in Customer department...");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("ADD_INTO_AIRLINE_COMPANY", conn);
                        cmd.Connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                       // SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                        cmd.Parameters.Add(new SqlParameter("@userName", company.USER_NAME));
                        cmd.Parameters.Add(new SqlParameter("@password", company.PASSWORD));
                        cmd.Parameters.Add(new SqlParameter("@airlineName", company.AIRLINE_NAME));
                        cmd.Parameters.Add(new SqlParameter("@countryCode", company.COUNTRY_CODE));
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
                catch(SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        public AirlineCompany GetAirlineByUserame(string name)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                AirlineCompany alc= new AirlineCompany();
                SqlCommand cmd = new SqlCommand("GetAirLineByUserName_sp", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userName", name));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                
                while (reader.Read()==true)
                {
                    alc = new AirlineCompany
                    {
                        ID = (long)reader["ID"],
                        USER_NAME = (string)reader["USER_NAME"],
                        PASSWORD = (string)reader["PASSWORD"],
                        AIRLINE_NAME = (string)reader["AIRLINE_NAME"],
                        COUNTRY_CODE = (long)reader["COUNTRY_CODE"]
                    };
                    
                }
                cmd.Connection.Close();
                return alc;
            }
        }

        public List<AirlineCompany> GetAllAirlinesByCountry(int countryId)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                AirlineCompany alc = new AirlineCompany();
                List<AirlineCompany> Lalc = new List<AirlineCompany>();
                SqlCommand cmd = new SqlCommand("GetAirLineByCountry", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    alc = new AirlineCompany
                    {
                        ID = (long)reader["ID"],
                        USER_NAME = (string)reader["USER_NAME"],
                        PASSWORD = (string)reader["PASSWORD"],
                        AIRLINE_NAME = (string)reader["AIRLINE_NAME"],
                        COUNTRY_CODE = (long)reader["COUNTRY_CODE"]
                    };
                    
                    Lalc.Add(alc);
                }
                cmd.Connection.Close();
                return Lalc;
            }
        }
        public bool ExUserNameInAirLineCompanies(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                long id = -1;
                SqlCommand cmd = new SqlCommand("EXISTING_USER_OR_NOT", conn);
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@userName", userName));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    id = (long)reader["ID"];
                };
                cmd.Connection.Close();
                if (id==-1)//there is no such a user - no multiple user name;
                {
                    Console.WriteLine("User name can be used");
                    return false;
                }
                return true; // User name is exist AirLine Companies table.
            }
        }

        public bool ExUserNameInCustomers(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                long id = -1;
                SqlCommand cmd = new SqlCommand("EXISTING_USER_IN_CUSTOMER", conn);
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@userName", userName));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    id = (long)reader["ID"];
                };
                cmd.Connection.Close();
                if (id == -1)//there is no such a user - no multiple user name;
                {
                    Console.WriteLine("User name can be used");
                    return false;
                }
                return true;
            }
        }

        public AirlineCompany Get(int id) // ?
        {
            AirlineCompany company = new AirlineCompany();
            using (SqlConnection conn = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("GetAirLineById", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read()==true)
                {
                    company.ID = (long)reader["ID"];
                    company.AIRLINE_NAME = (string)reader["AIRLINE_NAME"];
                    company.USER_NAME = (string)reader["USER_NAME"];
                    company.PASSWORD = (string)reader["PASSWORD"];
                    company.COUNTRY_CODE = (long)reader["COUNTRY_CODE"];
                }
                cmd.Connection.Close();
            }
            return company;
        }

        public List<AirlineCompany> GetAll()
        {
            AirlineCompany alc = new AirlineCompany();
            List<AirlineCompany> Lalc = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllAirlineCompanies", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    alc = new AirlineCompany
                    {
                        ID = (long)reader["ID"],
                        USER_NAME = (string)reader["USER_NAME"],
                        PASSWORD = (string)reader["PASSWORD"],
                        AIRLINE_NAME = (string)reader["AIRLINE_NAME"],
                        COUNTRY_CODE = (long)reader["COUNTRY_CODE"]
                    };
                    Lalc.Add(alc);
                }
                cmd.Connection.Close();
                return Lalc;
            }
            throw new NotImplementedException();
        }

        public void Remove(AirlineCompany t)
        {
            if (ExUserNameInAirLineCompanies(t.USER_NAME) == true)
            {
                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("RemoveAirLineCompany", conn);
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@countryCode", t.COUNTRY_CODE));
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            else
            {
                Console.WriteLine($"There is no such a company - {t.AIRLINE_NAME}");
            }
        }

        public void Update(AirlineCompany t)
        {
            if (ExUserNameInAirLineCompanies(t.USER_NAME) == true)
            {
                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateAirLineCompany", conn);
                    cmd.Connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@AirLineName", t.AIRLINE_NAME));
                    cmd.Parameters.Add(new SqlParameter("@AirLineUserName", t.USER_NAME));
                    cmd.Parameters.Add(new SqlParameter("@AirLinePasswprd", t.PASSWORD));
                    cmd.Parameters.Add(new SqlParameter("@CountryCode", t.COUNTRY_CODE));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            else
            {
                Console.WriteLine($"There is no such a company - {t.AIRLINE_NAME}");
            }
        }
        public void RemoveAll()
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM AirlineCompanies", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.BeginExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        public long GetAirlineCompanyId(string airlineUserName)
        {
            long id = 0;
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAirlineCompanyId", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@airlineUserName", airlineUserName));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    id = (long)reader["ID"];
                }
                cmd.Connection.Close();
            }
            return id;
        }
        public void ChangePassword(string userName,string password)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE AirlineCompanies SET PASSWORD = {password} WHERE USER_NAME = {userName}", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Close();
            }
        }
    }
}
