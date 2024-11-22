using BusinessObject.DTOs;
using DataAccess.DAO;
using Microsoft.Extensions.Configuration;

namespace Repositories.Event
{
    public class EventRepository : IEventRepository
    {
        public List<EventDTO> GetAllEvents()
        {
            return EventDAO.GetAllEvents();
        }
        public List<EventDTO> GetEventByCreatedBy(int id)
        {
            return EventDAO.GetEventByCreatedBy(id);
        }
        public EventDTO GetEventById(int id)
        {
            return EventDAO.GetEventById(id);
        }
        public async Task<bool> AddEvent(CreateEventDTO eventDTO, IConfiguration configuration)
        {
            return await EventDAO.AddEvent(eventDTO, configuration);
        }

        public bool UpdateEvent(UpdateEventDTO eventDTO)
        {
            return EventDAO.UpdateEvent(eventDTO);
        }

        public bool DeleteEvent(int eventId)
        {
            return EventDAO.DeleteEvent(eventId);
        }

        public List<EventDTO> GetTop3NewestEvents()
        {
            return EventDAO.GetTop3NewestEvents();
        }
    }
}
