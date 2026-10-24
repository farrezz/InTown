using InTown.Core.Models;

namespace InTown.Core.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAll();
    Task<Event?> GetById(int id);
    Task<Event> Add(Event ev);
    Task Update(Event ev);
    Task Delete(Event ev);
    Task<bool> IsAttending(int eventId, int userId);
    Task AddAttendee(Attendee attendee);
    
}