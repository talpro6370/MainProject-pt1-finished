using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.DAO
{
    public class FlightDAOMSSQL : IFlightDAO
    {
        
        
        public void Add(Flight flight)
        {
            if (ExistInFlights(flight) == false)
            {
                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                {

                    SqlCommand cmd = new SqlCommand("Add_INTO_FLIGHTS", conn);
                    cmd.Connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@airlineCompanyId", flight.AIRLINECOMPANY_ID));
                    cmd.Parameters.Add(new SqlParameter("@originCountryCode", flight.ORIGIN_COUNTRY_CODE));
                    cmd.Parameters.Add(new SqlParameter("@destinationCountryCode", flight.DESTINATION_COUNTRY_CODE));
                    cmd.Parameters.Add(new SqlParameter("@departureTime", flight.DEPARTURE_TIME));
                    cmd.Parameters.Add(new SqlParameter("@landingTime", flight.LANDING_TIME));
                    cmd.Parameters.Add(new SqlParameter("@remainingTickets", flight.REMAINING_TICKETS));
                    cmd.Parameters.Add(new SqlParameter("@airlineCompanyName", flight.AIRLINECOMPANY_NAME));
                    cmd.Parameters.Add(new SqlParameter("@flightName", flight.FLIGHT_NAME));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            else
            {
                Console.WriteLine("The flight is already exist");
            }
        }

        public bool ExistInFlights(Flight flight)
        {
            long recId=0;
            long realId = GetFlightId(flight.FLIGHT_NAME);
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("flightExistInDb", conn);
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@flightId", realId));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                
                while (reader.Read()==true)
                {
                    recId = (long)reader["ID"];
                }
                if(recId ==0)
                {
                    return false; 
                }
                return true;
            }
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> vacancyFlights = new Dictionary<Flight, int>();
            Flight flight;
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllFlightsVacancy", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while(reader.Read()==true)
                {
                    flight = new Flight
                    {
                        ID = (long)reader["ID"],
                        AIRLINECOMPANY_ID = (long)reader["AIRLINECOMPANY_ID"],
                        ORIGIN_COUNTRY_CODE = (long)reader["ORIGIN_COUNTRY_CODE"],
                        DESTINATION_COUNTRY_CODE = (long)reader["DESTINATION_COUNTRY_CODE"],
                        DEPARTURE_TIME = (DateTime)reader["DEPARTURE_TIME"],
                        LANDING_TIME = (DateTime)reader["LANDING_TIME"],
                        REMAINING_TICKETS = (int)reader["REMAINING_TICKETS"],
                        AIRLINECOMPANY_NAME = (string)reader["AIRLINECOMPANY_NAME"]
                    };
                    vacancyFlights.Add(flight, (int)flight.ID);
                }
                cmd.Connection.Close();
            }
            return vacancyFlights;
        }

        public Flight GetFlightById(int id)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFlightById", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    return new Flight((long)reader["AIRLINECOMPANY_ID"], (long)reader["ORIGIN_COUNTRY_CODE"], (long)reader["DESTINATION_COUNTRY_CODE"], (DateTime)reader["DEPARTURE_TIME"], (DateTime)reader["LANDING_TIME"], (int)reader["REMAINING_TICKETS"], (string)reader["AIRLINECOMPANY_NAME"],(string)reader["FLIGHT_NAME"]);
                }
                cmd.Connection.Close();
            }
            return null;
        }
        public List<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> flightsList = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFlightsByCustomer", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customerId", customer.ID));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    flightsList.Add(new Flight((long)reader["AIRLINECOMPANY_ID"], (long)reader["ORIGIN_COUNTRY_CODE"], (long)reader["DESTINATION_COUNTRY_CODE"], (DateTime)reader["DEPARTURE_TIME"], (DateTime)reader["LANDING_TIME"], (int)reader["REMAINING_TICKETS"], (string)reader["AIRLINECOMPANY_NAME"], (string)reader["FLIGHT_NAME"]));
                }
                cmd.Connection.Close();
            }
            return flightsList;
        }

        public List<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFlightsByDepatrureDate", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@depatrureDate", departureDate));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    flights.Add(new Flight((long)reader["AIRLINECOMPANY_ID"], (long)reader["ORIGIN_COUNTRY_CODE"], (long)reader["DESTINATION_COUNTRY_CODE"], (DateTime)reader["DEPARTURE_TIME"], (DateTime)reader["LANDING_TIME"], (int)reader["REMAINING_TICKETS"], (string)reader["AIRLINECOMPANY_NAME"], (string)reader["FLIGHT_NAME"]));
                }
                cmd.Connection.Close();
            }
            return flights;
        }
        public List<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFlightsByDestinationCountry", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@countryCode", countryCode));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    flights.Add(new Flight((long)reader["AIRLINECOMPANY_ID"], (long)reader["ORIGIN_COUNTRY_CODE"], (long)reader["DESTINATION_COUNTRY_CODE"], (DateTime)reader["DEPARTURE_TIME"], (DateTime)reader["LANDING_TIME"], (int)reader["REMAINING_TICKETS"], (string)reader["AIRLINECOMPANY_NAME"], (string)reader["FLIGHT_NAME"]));
                }
                cmd.Connection.Close();
            }
            return flights;
        }
        public List<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFlightsByLandingDate", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@landingDate", landingDate));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    flights.Add(new Flight((long)reader["AIRLINECOMPANY_ID"], (long)reader["ORIGIN_COUNTRY_CODE"], (long)reader["DESTINATION_COUNTRY_CODE"], (DateTime)reader["DEPARTURE_TIME"], (DateTime)reader["LANDING_TIME"], (int)reader["REMAINING_TICKETS"], (string)reader["AIRLINECOMPANY_NAME"], (string)reader["FLIGHT_NAME"]));
                }
                cmd.Connection.Close();
            }
            return flights;
        }

        public List<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFlightsByOriginCountry", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@countryCode", countryCode));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    flights.Add(new Flight((long)reader["AIRLINECOMPANY_ID"], (long)reader["ORIGIN_COUNTRY_CODE"], (long)reader["DESTINATION_COUNTRY_CODE"], (DateTime)reader["DEPARTURE_TIME"], (DateTime)reader["LANDING_TIME"], (int)reader["REMAINING_TICKETS"], (string)reader["AIRLINECOMPANY_NAME"], (string)reader["FLIGHT_NAME"]));
                }
                cmd.Connection.Close();
            }
            return flights;
        }
        public List<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllFlights", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    flights.Add(new Flight((long)reader["AIRLINECOMPANY_ID"], (long)reader["ORIGIN_COUNTRY_CODE"], (long)reader["DESTINATION_COUNTRY_CODE"], (DateTime)reader["DEPARTURE_TIME"], (DateTime)reader["LANDING_TIME"], (int)reader["REMAINING_TICKETS"], (string)reader["AIRLINECOMPANY_NAME"], (string)reader["FLIGHT_NAME"]));
                }
                cmd.Connection.Close();
            }
            return flights;
        }
        public void Update(Flight flight)
        {
            if (ExflightInFlights(flight) == true)
            {
                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateFlight", conn);
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    cmd.Parameters.Add(new SqlParameter("@AirLineCompanyId", flight.AIRLINECOMPANY_ID));
                    cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", flight.ORIGIN_COUNTRY_CODE));
                    cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", flight.DESTINATION_COUNTRY_CODE));
                    cmd.Parameters.Add(new SqlParameter("@DEPARTURE_TIME", flight.DEPARTURE_TIME));
                    cmd.Parameters.Add(new SqlParameter("@LANDING_TIME", flight.LANDING_TIME));
                    cmd.Parameters.Add(new SqlParameter("@REMAINING_TICKETS", flight.REMAINING_TICKETS));
                    cmd.Parameters.Add(new SqlParameter("@flightName", flight.FLIGHT_NAME));
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            else
            {
                Console.WriteLine("There is no such a flight");
            }
        }
        public void Remove(Flight flight)
        {
            if(ExflightInFlights(flight)==true)
            {
                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("RemoveFlight", conn);
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@flightId", flight.ID));
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
        }
        public bool ExflightInFlights(Flight flight)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                long realId = GetFlightId(flight.FLIGHT_NAME);
                long id = -1;
                SqlCommand cmd = new SqlCommand("ExFlight", conn);
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@flightId", realId));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    id = (long)reader["ID"];
                };
                cmd.Connection.Close();
                if (id == -1)//there is no such a flight
                {
                    return false;
                }
                return true; 
            }
        }
        public void ListOfFlights()
        {
            List<Flight> flights = GetAll();
            
        }

        public Flight Get(int id)
        {
            throw new NotImplementedException();
        }

        public long GetFlightId(string flightName)
        {
            long id = 0;
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFlightIdByFlightName", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@flightName", flightName));
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
