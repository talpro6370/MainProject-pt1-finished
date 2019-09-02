using System.Collections.Generic;

namespace MainProject.Business_Logic
{
    internal interface ILoggedInAirlineFacade
    {
        List<Tickets> GetAllTickets(LoginToken<AirlineCompany> token);
        List<Flight> GetAllFlights(LoginToken<AirlineCompany> token);
        void CancelFlight(LoginToken<AirlineCompany> token, Flight flight);
        void CreateFlight(LoginToken<AirlineCompany> token, Flight flight);
        void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight);
        void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword);
        void MofidyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline);
    }
}