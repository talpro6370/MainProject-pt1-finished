using MainProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Business_Logic
{
    
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade, ILoginTokenBase
    {
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if(token!=null)
            {
                if(flight.REMAINING_TICKETS>0)
                {
                    _ticketDAO = new TicketDAOMSSQL(); 
                    Tickets t = new Tickets();
                    t = _ticketDAO.GetTicketByFlightId((int)flight.ID);
                    _ticketDAO.Remove(t);
                }
                if (flight.REMAINING_TICKETS == 0)
                {
                    _flightDAO = new FlightDAOMSSQL();
                    _flightDAO.Remove(flight);
                }
            }
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (token!=null)
            {
                if(token.User.PASSWORD==oldPassword)
                {
                    token.User.PASSWORD = newPassword;
                    _airlineDAO.ChangePassword(token.User.USER_NAME, newPassword);
                }
            }
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if(token!=null)
            {
                _flightDAO = new FlightDAOMSSQL();
                _flightDAO.Add(flight);
            }
        }

        public List<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            List<Flight> resList = new List<Flight>();
            if (token!=null)
            {
                _flightDAO = new FlightDAOMSSQL();
                resList= _flightDAO.GetAll();
            }
            return resList;
        }

        public List<Tickets> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            List<Tickets> tickets = new List<Tickets>();
            if (token!=null)
            {
                _ticketDAO = new TicketDAOMSSQL();
                tickets = _ticketDAO.GetAll();
            }
            return tickets;
        }

        public void MofidyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (token != null)
            {
                _airlineDAO = new AirLineDAOMSSQL();
                _airlineDAO.Update(airline);
            }
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null)
            {
                _flightDAO = new FlightDAOMSSQL();
                _flightDAO.Update(flight);
            }
        }
        public AirlineCompany GetAirlineByUserName(LoginToken<AirlineCompany> token ,string userName)
        {
            if(token!=null)
            {
                _airlineDAO = new AirLineDAOMSSQL();
              return  _airlineDAO.GetAirlineByUserame(userName);
            }
            return null;
        }
        public long GetAirLineIdByUserName(string userName)
        {
            return _airlineDAO.GetAirlineCompanyId(userName);
        }

    }
}
