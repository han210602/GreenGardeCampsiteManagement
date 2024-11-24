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
        public (List<TicketDTO> Tickets, int TotalPages) GetTicketsByCategoryIdAndSort(int? categoryId, int? sort, int page, int pageSize);
        void AddTicket(AddTicket ticketDto);
        void UpdateTicket(UpdateTicket ticketDto);
        void ChangeTicketStatus(int ticketId);
    }
}
