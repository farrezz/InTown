using InTown.Core.Models;

namespace InTown.Core.Interfaces;

public interface IEventService
{
    Task<List<Event>> GetUpcoming();
    Task<Event?> GetById(int id);
    Task<Event> Create(string title, string location, string description, DateTime startDate, DateTime endDate, int userId);
    Task<bool> Update (int eventId, int userId, string title, string location, string description, DateTime startDate, DateTime endDate);
    Task<bool> Delete(int eventId,  int userId);
    Task<AttendResult> Attend(int eventId, int userId);
}