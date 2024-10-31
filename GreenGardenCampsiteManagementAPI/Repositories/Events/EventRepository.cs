using BusinessObject.DTOs;
using DataAccess.DAO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Events
{
    public class EventRepository:IEventRepository
    {
        public List<EventDTO> GetAllEvents()
        {
            return EventDAO.GetAllEvents();
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
    }
}
