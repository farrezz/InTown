namespace InTown.API.DTOS;

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Location { get; set; } = "";
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    public string? ImageUrl { get; set; }
    public string CreatedByName { get; set; } = "";
    public int AttendeeCount { get; set; }
}