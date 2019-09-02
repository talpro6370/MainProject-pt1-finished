using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
   public class Tickets : IPoco
    {
        public long ID { get; set; }
        public long FLIGHT_ID { get; set; }
        public long CUSTOMER_ID { get; set; }
        public Tickets()
        {

        }

        public Tickets(long fLIGHT_ID, long cUSTOMER_ID)
        {
            FLIGHT_ID = fLIGHT_ID;
            CUSTOMER_ID = cUSTOMER_ID;
        }

        public override bool Equals(object obj)
        {
            var tickets = obj as Tickets;
            return tickets != null &&
                   FLIGHT_ID == tickets.FLIGHT_ID &&
                   CUSTOMER_ID == tickets.CUSTOMER_ID;
        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }

        public static bool operator ==(Tickets t1, Tickets t2)
        {
            if (ReferenceEquals(t1, null) && ReferenceEquals(t2, null))
            {
                return true;
            }
            if (ReferenceEquals(t1, null) || ReferenceEquals(t2, null))
            {
                return false;
            }
            if (t1.CUSTOMER_ID==t2.CUSTOMER_ID&&t1.FLIGHT_ID==t2.FLIGHT_ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Tickets t1, Tickets t2)
        {
            return !(t1==t2);
        }

    }
}
