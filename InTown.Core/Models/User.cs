namespace InTown.Core.Models;

public class User
{
    //public Guid Id { get; set; }
    //int instead of Guid because easy to remember when testing in swagger
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    
    //with hash
    public string Password { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    
    //showcases user´s created events
    public List<Event> CreatedEvents { get; set; } = new();
}