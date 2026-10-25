using System.Security.Claims;
using InTown.API.DTOS;
using InTown.Core.Interfaces;
using InTown.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTown.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetUpcoming();
        
        var result = new List<EventDto>();
        foreach (var e in events)
        {
            result.Add(ToDto(e));
            
        }
        return Ok(result);
        
    }
    
    // GET /api/events/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ev = await _eventService.GetById(id);
        if (ev == null)
            return NotFound();
        return Ok(ToDetailDto(ev));
    }
    
    // POST /api/events
    [HttpPost]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {
        int userId = GetUserId();
        var ev = await _eventService.Create(request.Title, request.Location, request.Description, request.StartDate, request.EndDate, userId);
        return Ok(ToDto(ev));
    }
    
// PUT /api/events/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateEventRequest request)
    {
        int userId = GetUserId();
        bool updated = await _eventService.Update(id, userId, request.Title, request.Location, request.Description, request.StartDate, request.EndDate);
        if (!updated)
            return Forbid(); // not found, or not their event
        return NoContent();
    }
// DELETE /api/events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int userId = GetUserId();
        bool deleted = await _eventService.Delete(id, userId);
        if (!deleted)
            return Forbid(); // not found, or not their event
        return NoContent();
    }
    
    // POST /api/events/5/attend
    [HttpPost("{id}/attend")]
    public async Task<IActionResult> Attend(int id)
    {
        int userId = GetUserId();
        AttendResult result = await _eventService.Attend(id, userId);
        if (result == AttendResult.NotFound)
            return NotFound();
        if (result == AttendResult.AlreadyGoing)
            return BadRequest("You are already going");
        return Ok();
    } 
    
    // read the user id from the token (the wristband)
    private int GetUserId()
    {   
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
// mapping: copy values from the model into the DTO
    private EventDto ToDto(Event e)
    {
        var dto = new EventDto();
        dto.Id = e.Id;
        dto.Title = e.Title;
        dto.Location = e.Location;
        dto.StartDate = e.StartDate;
        dto.EndDate = e.EndDate;
        dto.ImageUrl = e.ImageUrl;
        dto.CreatedByName = e.CreatedBy.Name; // reach through the navigation property
        dto.AttendeeCount = e.Attendees.Count; // count the list
        return dto;
    }

    private EventDetailDto ToDetailDto(Event e)
    {
        var dto = new EventDetailDto();
        dto.Id = e.Id;
        dto.Title = e.Title;
        dto.Location = e.Location;
        dto.StartDate = e.StartDate;
        dto.EndDate = e.EndDate;
        dto.ImageUrl = e.ImageUrl;
        dto.CreatedByName = e.CreatedBy.Name;
        dto.AttendeeCount = e.Attendees.Count;
        dto.Description = e.Description;
        dto.CreatedByUserId = e.CreatedByUserId;
        foreach (var a in e.Attendees)
        {
            dto.AttendeeNames.Add(a.User.Name);

        }

        return dto;
    }

}