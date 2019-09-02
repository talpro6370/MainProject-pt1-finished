using MainProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.InterFaces
{
    public interface ITicketDAO:IBasicDB<Tickets>
    {
        Tickets GetTicketByFlightId(int id);
    }
}
