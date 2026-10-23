namespace InTown.Core.Models;

public class Attendee
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
}