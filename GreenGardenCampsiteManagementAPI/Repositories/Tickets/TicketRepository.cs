using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Tickets
{
    public class TicketRepository : ITicketRepository
    {
        public List<TicketDTO> GetAllTickets()
        {
            return TicketDAO.GetAllTickets();
        }
        public TicketDTO GetTicketDetail(int id)
        {
            return TicketDAO.GetTicketDetail(id);
        }
        public List<TicketDTO> GetTicketsByCategoryIdAndSort(int? categoryId, int? sort)
        {
            return TicketDAO.GetTicketsByCategoryIdAndSort(categoryId, sort); // Gọi phương thức từ TicketDAO
        }
        public void AddTicket(AddTicket ticketDto)
        {
            TicketDAO.AddTicket(ticketDto);
        }

        public void UpdateTicket(UpdateTicket ticketDto)
        {
            TicketDAO.UpdateTicket(ticketDto);
        }
        public List<TicketCategoryDTO> GetAllTicketCategories()
        {
            return TicketDAO.GetAllTicketCategories();
        }
        public void ChangeTicketStatus(int ticketId, ChangeTicketStatus newStatus)
        {
            TicketDAO.ChangeTicketStatus(ticketId, newStatus); // Call DAO method
        }
    }
}
