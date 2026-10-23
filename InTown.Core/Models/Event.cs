namespace InTown.Core.Models;

public class Event
{
    //base
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Location { get; set; }  = "";
    public string Description { get; set; } = "";   
    
    // start and end time for the event. 
    public DateTime StartDate { get; set; }
    
   // TODO (Phase 4, EventService + DTOs):
   // - Replace every use of the old "Date" with StartDate (and EndDate).
   //   Check: CreateEventRequest, EventDto, EventDetailDto, EventService, EventsController, CreateEventPage.jsx
   // - Upcoming rule: show an event while it hasn't ended yet, i.e. EndDate >= DateTime.Now
   //   (use Now, not Today, so an event ending this afternoon still shows)
   // - Validation: EndDate must not be before StartDate, return BadRequest("End must be after start")
   // - Send and return EndDate in every DTO
   // - Run a new migration after model changes (name it, e.g. AddEventEndDate) - if no migration has run yet, just recreate Init
    public DateTime EndDate { get; set; }
    
    //User can upload image (?)
    public string? ImageUrl { get; set; }
    
    //Who created it
    public int CreatedByUserId { get; set; }
    public User CreatedBy { get; set; }  = null!;
    
    //// - Set CreatedAt = DateTime.UtcNow in EventService.Create
    public DateTime CreatedOn { get; set; }
    
    
    //who is going
    public List<Attendee> Attendees { get; set; } = new();
}