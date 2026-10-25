namespace InTown.API.DTOS;

//EventDto = it has everything EventDto has, plus the lines below
public class EventDetailDto : EventDto
{
    public string Description { get; set; } = "";
    public int CreatedByUserId { get; set; } // React uses this to show Edit/Delete only to the owner
    public List<string> AttendeeNames { get; set; } = new();
}