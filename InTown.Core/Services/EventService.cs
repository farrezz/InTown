using System.Text;
using InTown.Core.Interfaces;
using InTown.Core.Models;

namespace InTown.Core.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepo;

    public EventService(IEventRepository eventRepo)
    {
        _eventRepo = eventRepo;
    }
        
    
    public async Task<List<Event>> GetUpcoming()
    {
        var all = await _eventRepo.GetAll();
        
        //rule: only show events that haven´t happened yet
        //TODO: lookover this rule
        var resultat = new List<Event>();
        foreach (var e in all)
        {
            if (e.StartDate  >= DateTime.Today)
                resultat.Add(e);
        }
        return resultat;
        
    }

    public async Task<Event?> GetById(int id)
    {
        return await _eventRepo.GetById(id);
    }

    public async Task<Event> Create(string title, string location, string description, DateTime startDate, DateTime endDate, int userId)
    {
        var ev = new Event();
        ev.Title = title;
        ev.Location = location;
        ev.Description = description;
        ev.StartDate = startDate;
        ev.EndDate = endDate;
        ev.CreatedByUserId = userId;
        
        var saved = await _eventRepo.Add(ev);   
        
        //load it again so createdby is filled in (the controller needs the creator´s name)
        var full=await _eventRepo.GetById(ev.Id);
        return full!;
    }

    public async Task<bool> Update(int eventId, int userId, string title, string location, string description, DateTime startDate,
        DateTime endDate)
    {
        var ev = await _eventRepo.GetById(eventId);
        if (ev == null)
            return false;
        
        //rule: only the person who created it can change it
        if (ev.CreatedByUserId != userId)
            return false;
        ev.Title = title;
        ev.Location = location;
        ev.Description = description;
        ev.StartDate = startDate;
        ev.EndDate = endDate;
        
        await _eventRepo.Update(ev);
        return true;
    }

    public async Task<bool> Delete(int eventId, int userId)
    {
        var ev = await _eventRepo.GetById(eventId);
        if (ev == null)
            return false;
        
        //rule: only the person who created it can delete it
        if (ev.CreatedByUserId != userId)
            return false;
        
        await _eventRepo.Delete(ev);
        return true;
    }

    public async Task<AttendResult> Attend(int eventId, int userId)
    {
        var ev = await _eventRepo.GetById(eventId);
        if (ev == null)
            return AttendResult.AlreadyGoing;

        var attendee = new Attendee();
        attendee.EventId = eventId;
        attendee.UserId = userId;
        
        await _eventRepo.AddAttendee(attendee);
        return AttendResult.Ok;

    }
}