using BusinessObject.DTOs;
using Microsoft.Extensions.Configuration;

namespace Repositories.Event
{
    public interface IEventRepository
    {
        List<EventDTO> GetAllEvents();
        List<EventDTO> GetEventByCreatedBy(int id);
        Task<bool> AddEvent(CreateEventDTO eventDTO, IConfiguration configuration);
        bool UpdateEvent(UpdateEventDTO eventDTO);
        bool DeleteEvent(int eventId);
        EventDTO GetEventById(int id);
        List<EventDTO> GetTop3NewestEvents();
    }
}
