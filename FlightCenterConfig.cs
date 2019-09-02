using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public static class FlightCenterConfig
    {
        public const string connectionString = @"Data Source = DESKTOP-0NBGVN2\MSSQLSERVER01;Initial Catalog = Main Project Db; integrated security = true";

        public static void DeleteDataBase()
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete from Tickets", conn);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlCommand cmd2 = new SqlCommand("delete from Flights", conn);
                cmd2.CommandType = System.Data.CommandType.Text;
                SqlCommand cmd3 = new SqlCommand("delete from AirlineCompanies", conn);
                cmd3.CommandType = System.Data.CommandType.Text;
                SqlCommand cmd4 = new SqlCommand("delete from Customers", conn);
                cmd4.CommandType = System.Data.CommandType.Text;
                SqlCommand cmd5 = new SqlCommand("delete from Countries", conn);
                cmd5.CommandType = System.Data.CommandType.Text;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                cmd2.Connection.Open();
                cmd2.ExecuteNonQuery();
                cmd2.Connection.Close();

                cmd3.Connection.Open();
                cmd3.ExecuteNonQuery();
                cmd3.Connection.Close();

                cmd4.Connection.Open();
                cmd4.ExecuteNonQuery();
                cmd4.Connection.Close();

                cmd5.Connection.Open();
                cmd5.ExecuteNonQuery();
                cmd5.Connection.Close();

            }
        }

        // Delete arrangment: 
        // 1. airlineCompanies
        // 2. Countries
        // 3. Tickets
        // 4. Customers
        // 5. Flights

    }
    
}
