using BusinessObject.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Events
{
    public interface IEventRepository
    {
        List<EventDTO> GetAllEvents();
        Task<bool> AddEvent(CreateEventDTO eventDTO, IConfiguration configuration);
        bool UpdateEvent(UpdateEventDTO eventDTO);
        bool DeleteEvent(int eventId);
    }
}
