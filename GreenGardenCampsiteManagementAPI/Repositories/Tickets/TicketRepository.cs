using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Tickets
{
    public class TicketRepository:ITicketRepository
    {
        public List<TicketDTO> GetAllTickets()
        {
            return TicketDAO.GetAllTickets();
        }
        public List<TicketDTO> GetTicketsByCategoryId(int categoryId) 
        {
            return TicketDAO.GetTicketsByCategoryId(categoryId);
        }

        public void AddTicket(AddTicket ticketDto)
        {
            TicketDAO.AddTicket(ticketDto);
        }

        public void UpdateTicket(UpdateTicket ticketDto)
        {
            TicketDAO.UpdateTicket(ticketDto);
        }
    }
}
