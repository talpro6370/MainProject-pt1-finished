using MainProject.Business_Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainProject
{
    public class FlyingCenterSystem
    {
        private static FlyingCenterSystem _instance;
        private static object key = new object();
        public LoginService login = new LoginService();
        protected FlyingCenterSystem()
        {
            Thread thre = new Thread(() =>
            {
                Thread.Sleep(86400000);

                
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.connectionString))
            {
                    
                SqlCommand cmd = new SqlCommand("MoveTicketsToANewTable", conn);
                SqlCommand cmd2 = new SqlCommand("UpdateFlightsTable", conn);
                SqlCommand cmd3 = new SqlCommand("MoveFlightsToANewTable", conn);
                SqlCommand cmd4 = new SqlCommand("DeleteFlightsWhoMoreThanThree", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                cmd2.Connection.Open();
                cmd2.CommandType = CommandType.StoredProcedure;
                //SqlDataReader reader2 = cmd2.ExecuteReader(CommandBehavior.Default);
                cmd2.ExecuteNonQuery();
                cmd2.Connection.Close();

                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Connection.Open();
                cmd3.ExecuteNonQuery();
                cmd3.Connection.Close();

                cmd4.Connection.Open();
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.ExecuteNonQuery();
                cmd4.Connection.Close();
            }
            });
            //insert into Flights (AIRLINECOMPANY_ID,ORIGIN_COUNTRY_CODE,DESTINATION_COUNTRY_CODE,DEPARTURE_TIME,LANDING_TIME,REMAINING_TICKETS,AIRLINECOMPANY_NAME) values('10017','10008','10009','2019/07/29 20:00:00','2019/07/30 14:30:00',0,'Haga')
        }
        public static FlyingCenterSystem GetInstance() //singlton demands.
        {
            if(_instance==null)
            {
                lock(key)
                {
                    if(_instance == null)
                    {
                        _instance = new FlyingCenterSystem();
                    }
                }
            }
            return _instance;
        }
        public void TryLogin()
        {
            //login.TryAdminLogin(,,);
            //login.TryAirlineLogin(,,);
            //login.TryCustomerLogin(,,);
        }

        public FacadeBase GetFacade(ILoginTokenBase token)
        {
           
            if(token is LoginToken<Administrator>)
            {
                return new LoggedInAdministratorFacade();
            }
            if (token is LoginToken<AirlineCompany>)
            {
                return new LoggedInAirlineFacade();
            }
            if (token is LoginToken<Customer>)
            {
                return new LoggedInCustomerFacade();
            }
            else
            {
                return new AnonymousUserFacade();
            } 
        }
    }
}
