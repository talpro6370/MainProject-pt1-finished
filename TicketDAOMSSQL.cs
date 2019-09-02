using MainProject.InterFaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.DAO
{
    public class TicketDAOMSSQL: ITicketDAO
    {
        
        public TicketDAOMSSQL()
        {

        }
        public void Add(Tickets ticket)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddTicket", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@flightId", ticket.FLIGHT_ID));
                cmd.Parameters.Add(new SqlParameter("@customerId", ticket.CUSTOMER_ID));
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        public Tickets Get(int id)
        {
            Tickets ticket = new Tickets();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTicketById", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    ticket = new Tickets
                    {
                        ID = (long)reader["ID"],
                        FLIGHT_ID = (long)reader["FLIGHT_ID"],
                        CUSTOMER_ID = (long)reader["CUSTOMER_ID"]
                    };
                }
                cmd.Connection.Close();
                return ticket;
            }
        }
        public Tickets GetTicketByFlightId(int id)
        {
            Tickets ticket = new Tickets();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTicketByFlightId", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@flightId", id));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    ticket = new Tickets
                    {
                        ID = (long)reader["ID"],
                        FLIGHT_ID = (long)reader["FLIGHT_ID"],
                        CUSTOMER_ID = (long)reader["CUSTOMER_ID"]
                    };
                }
                cmd.Connection.Close();
                return ticket;
            }
        }
        public List<Tickets> GetAll()
        {
            Tickets ticket;
            List<Tickets> listOfTickets = new List<Tickets>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllTickets", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    ticket = new Tickets
                    {
                        ID = (long)reader["ID"],
                        FLIGHT_ID = (long)reader["FLIGHT_ID"],
                        CUSTOMER_ID = (long)reader["CUSTOMER_ID"]
                    };
                    listOfTickets.Add(ticket);
                }
                cmd.Connection.Close();
            }
            return listOfTickets;
        }
        public void Remove(Tickets ticket)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("RemoveTicket", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ticketId", ticket.ID));
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        public void Update(Tickets ticket)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateTicket", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ticket.ID));
                cmd.Parameters.Add(new SqlParameter("@flightId", ticket.FLIGHT_ID));
                cmd.Parameters.Add(new SqlParameter("@customerId", ticket.CUSTOMER_ID));
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }
}
