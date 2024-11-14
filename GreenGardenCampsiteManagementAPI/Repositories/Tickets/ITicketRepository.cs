using BusinessObject.DTOs;
using BusinessObject.Models;
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
        List<TicketDTO> GetAllCustomerTickets();
        TicketDTO GetTicketDetail(int id);
        List<TicketCategoryDTO> GetAllTicketCategories();
        List<TicketDTO> GetTicketsByCategoryIdAndSort(int? categoryId, int? sort);
        void AddTicket(AddTicket ticketDto);
        void UpdateTicket(UpdateTicket ticketDto);
        void ChangeTicketStatus(int ticketId);
    }
}
