using InTown.Core.Interfaces;
using InTown.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InTown.Data.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;


    public EventRepository(AppDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<Event>> GetAll()
    {
// Include = also load the creator and the attendees
        return await _context.Events
            .Include(e => e.CreatedBy)
            .Include(e => e.Attendees)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<Event?> GetById(int id)
    {
        //thenInclude = for each attendee, also load their user (names)
        return await _context.Events
            .Include(e => e.CreatedBy)
            .Include(e => e.Attendees)
            .ThenInclude(a => a.User)
            .FirstOrDefaultAsync(e => e.Id == id);

    }

    public async Task<Event> Add(Event ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
        return ev;
    }

    public async Task Update(Event ev)
    {
        _context.Events.Update(ev);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Event ev)
    {
        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsAttending(int eventId, int userId)
    {
        return await _context.Attendees.AnyAsync(a => a.EventId == eventId && a.UserId == userId);
    }

    public async Task AddAttendee(Attendee attendee)
    {
        _context.Attendees.Add(attendee);
        await _context.SaveChangesAsync();
    }
}