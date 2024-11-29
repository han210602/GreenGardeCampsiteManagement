using BusinessObject.DTOs;
using DataAccess.DAO;

namespace Repositories.Tickets
{
    public class TicketRepository : ITicketRepository
    {
        public List<TicketDTO> GetAllTickets()
        {
            return TicketDAO.GetAllTickets();
        }
        public List<TicketDTO> GetAllCustomerTickets()
        {
            return TicketDAO.GetAllCustomerTickets();
        }
        public TicketDTO GetTicketDetail(int id)
        {
            return TicketDAO.GetTicketDetail(id);
        }
        public (List<TicketDTO> Tickets, int TotalPages) GetTicketsByCategoryIdAndSort(int? categoryId, int? sortBy, int page, int pageSize)
        {
            return TicketDAO.GetTicketsByCategoryIdAndSort(categoryId, sortBy, page, pageSize);
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
        public void ChangeTicketStatus(int ticketId)
        {
            TicketDAO.ChangeTicketStatus(ticketId); // Call DAO method
        }
    }
}
