using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public class Flight : IPoco
    {
        public long ID { get; set; }
        public string AIRLINECOMPANY_NAME { get; set; }
        public long AIRLINECOMPANY_ID { get; set; }
        public long ORIGIN_COUNTRY_CODE { get; set; }
        public long DESTINATION_COUNTRY_CODE { get; set; }
        public DateTime DEPARTURE_TIME { get; set; }
        public DateTime LANDING_TIME { get; set; }
        public int REMAINING_TICKETS { get; set; }
        public string FLIGHT_NAME { get; set; }
        public Flight()
        {

        }
        public Flight(long aIRLINECOMPANY_ID ,long oRIGIN_COUNTRY_CODE, long dESTINATION_COUNTRY_CODE, DateTime dEPARTURE_TIME, DateTime lANDING_TIME, int rEMAINING_TICKETS, string aIRLINECOMPANY_NAME,string fLIGHT_NAME)
        {
            AIRLINECOMPANY_ID = aIRLINECOMPANY_ID;
            ORIGIN_COUNTRY_CODE = oRIGIN_COUNTRY_CODE;
            DESTINATION_COUNTRY_CODE = dESTINATION_COUNTRY_CODE;
            DEPARTURE_TIME = dEPARTURE_TIME;
            LANDING_TIME = lANDING_TIME;
            REMAINING_TICKETS = rEMAINING_TICKETS;
            AIRLINECOMPANY_NAME = aIRLINECOMPANY_NAME;
            FLIGHT_NAME = fLIGHT_NAME;
        }
        public override bool Equals(object obj)
        {
            var copyFlight = obj as Flight;
            bool v = ID == copyFlight.ID;
            return copyFlight != null &&
                   v &&
                   AIRLINECOMPANY_ID == copyFlight.AIRLINECOMPANY_ID &&
                   ORIGIN_COUNTRY_CODE == copyFlight.ORIGIN_COUNTRY_CODE &&
                   DESTINATION_COUNTRY_CODE == copyFlight.DESTINATION_COUNTRY_CODE &&
                   DEPARTURE_TIME == copyFlight.DEPARTURE_TIME && LANDING_TIME == copyFlight.LANDING_TIME &&
                   REMAINING_TICKETS == copyFlight.REMAINING_TICKETS && FLIGHT_NAME == copyFlight.FLIGHT_NAME;
        }
        public static bool operator ==(Flight f1, Flight f2)
        {
            if (ReferenceEquals(f1, null) && ReferenceEquals(f2, null))
            {
                return true;
            }
            if (ReferenceEquals(f1, null) || ReferenceEquals(f2, null))
            {
                return false;
            }
            if (f1.AIRLINECOMPANY_ID==f1.AIRLINECOMPANY_ID&&f1.DEPARTURE_TIME==f2.DEPARTURE_TIME&&f1.DESTINATION_COUNTRY_CODE==f2.DESTINATION_COUNTRY_CODE&&f1.LANDING_TIME==f2.LANDING_TIME&&f1.ORIGIN_COUNTRY_CODE==f2.ORIGIN_COUNTRY_CODE&&f1.REMAINING_TICKETS==f2.REMAINING_TICKETS && f1.FLIGHT_NAME == f2.FLIGHT_NAME)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Flight a1, Flight a2)
        {
            return !(a1 == a2);
        }
        public override int GetHashCode()
        {
            return (int)this.ID;
        }
        public override string ToString()
        {
            return $"Airline company: {this.AIRLINECOMPANY_ID}";
        }
    }
}
