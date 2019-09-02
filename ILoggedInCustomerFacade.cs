using System.Collections.Generic;

namespace MainProject.Business_Logic
{
    internal interface ILoggedInCustomerFacade
    {
        List<Flight> GetAllMyFlights(LoginToken<Customer> token);
        Tickets PurchaseTicket(LoginToken<Customer> token, Flight flight);
        void CancelTicket(LoginToken<Customer> token, Tickets ticket);
    }
}