using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public class CustomerDAOMSSQL:ICustomerDAO
    {
       
        public CustomerDAOMSSQL()
        {

        }
        public void Add(Customer customer)
        {
            AirLineDAOMSSQL ald = new AirLineDAOMSSQL();
            if (ald.ExUserNameInCustomers(customer.USER_NAME) == true)
            {
                Console.WriteLine("The user name is already exist in airline companies department...");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("ADD_INTO_CUSTOMER", conn);
                        cmd.Connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@firstName", customer.FIRST_NAME));
                        cmd.Parameters.Add(new SqlParameter("@lastName", customer.LAST_NAME));
                        cmd.Parameters.Add(new SqlParameter("@userName", customer.USER_NAME));
                        cmd.Parameters.Add(new SqlParameter("@password", customer.PASSWORD));
                        cmd.Parameters.Add(new SqlParameter("@address", customer.ADDRESS));
                        cmd.Parameters.Add(new SqlParameter("@phoneNo", customer.PHONE_NO));
                        cmd.Parameters.Add(new SqlParameter("@creditCardNumber", customer.CREDIT_CARD_NUMBER));
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        public Customer GetCustomerByUserame(string name)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                Customer alc = new Customer();
                SqlCommand cmd = new SqlCommand("GetCustomerByUserName", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userName", name));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    alc = new Customer
                    {
                        ID = (long)reader["ID"],
                        USER_NAME = (string)reader["USER_NAME"],
                        PASSWORD = (string)reader["PASSWORD"],
                        FIRST_NAME = (string)reader["FIRST_NAME"],
                        LAST_NAME = (string)reader["LAST_NAME"],
                        ADDRESS = (string)reader["ADDRESS"],
                        PHONE_NO = (string)reader["PHONE_NO"],
                        CREDIT_CARD_NUMBER=(string)reader["CREDIT_CARD_NUMBER"]
                    };

                }
                cmd.Connection.Close();
                return alc;
            }

        }
        public Customer Get(int id) // ?
        {
            Customer customer = new Customer();
            using (SqlConnection conn = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("GetCustomerById", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    customer.ID = (long)reader["ID"];
                    customer.FIRST_NAME = (string)reader["FIRST_NAME"];
                    customer.LAST_NAME = (string)reader["LAST_NAME"];
                    customer.USER_NAME = (string)reader["USER_NAME"];
                    customer.PASSWORD = (string)reader["PASSWORD"];
                    customer.ADDRESS = (string)reader["ADDRESS"];
                    customer.PHONE_NO = (string)reader["PHONE_NO"];
                    customer.CREDIT_CARD_NUMBER = (string)reader["CREDIT_CARD_NUMBER"];
                }
                cmd.Connection.Close();
            }
            return customer;
        }
        public List<Customer> GetAll()
        {
            Customer customer = new Customer();
            List<Customer> Lcustomer = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllCustomers", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read() == true)
                {
                    customer = new Customer
                    {
                        ID = (long)reader["ID"],
                        FIRST_NAME = (string)reader["FIRST_NAME"],
                        LAST_NAME = (string)reader["LAST_NAME"],
                        USER_NAME = (string)reader["USER_NAME"],
                        PASSWORD = (string)reader["PASSWORD"],
                        ADDRESS = (string)reader["ADDRESS"],
                        PHONE_NO = (string)reader["PHONE_NO"],
                        CREDIT_CARD_NUMBER = (string)reader["CREDIT_CARD_NUMBER"]
                    };
                    Lcustomer.Add(customer);
                }
                cmd.Connection.Close();
                return Lcustomer;
            }
        }
        public void Remove(Customer customer)
        {
            AirLineDAOMSSQL ald = new AirLineDAOMSSQL();
            if (ald.ExUserNameInCustomers(customer.USER_NAME) == true)
            {
                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("RemoveCustomer", conn);
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@password", customer.PASSWORD));
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            else
            {
                Console.WriteLine($"There is no such a customer - {customer.FIRST_NAME}");
            }
        }
        public void Update(Customer customer)
        {
            AirLineDAOMSSQL ald = new AirLineDAOMSSQL();
            if (ald.ExUserNameInCustomers(customer.USER_NAME) == true)
            {
                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateCustomer", conn);
                    cmd.Connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@firstName", customer.FIRST_NAME));
                    cmd.Parameters.Add(new SqlParameter("@lastName", customer.LAST_NAME));
                    cmd.Parameters.Add(new SqlParameter("@userName", customer.USER_NAME));
                    cmd.Parameters.Add(new SqlParameter("@password", customer.PASSWORD));
                    cmd.Parameters.Add(new SqlParameter("@address", customer.ADDRESS));
                    cmd.Parameters.Add(new SqlParameter("@phone_No", customer.PHONE_NO));
                    cmd.Parameters.Add(new SqlParameter("@creaditCardNumber", customer.CREDIT_CARD_NUMBER));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            else
            {
                Console.WriteLine($"There is no such a company - {customer.FIRST_NAME}");
            }
        }
        public long GetCustomerId(string customerUserName)
        {
            long id = 0;
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCustomerIdByUserName", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customerUserName", customerUserName));
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
