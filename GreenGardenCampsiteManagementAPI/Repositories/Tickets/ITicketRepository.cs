using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Tickets
{
    public interface ITicketRepository
    {
        List<TicketDTO> GetAllTickets();
        List<TicketDTO> GetTicketsByCategoryId(int categoryId);
        void AddTicket(AddTicket ticketDto);
        void UpdateTicket(UpdateTicket ticketDto);
    }
}
