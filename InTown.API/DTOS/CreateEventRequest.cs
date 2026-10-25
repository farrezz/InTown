using System.ComponentModel.DataAnnotations;

namespace InTown.API.DTOS;

public class CreateEventRequest
{
    
    [Required, MaxLength(100)]
    public string Title { get; set; } = "";
    
    [Required]
    public string Location { get; set; } = "";
    
    public string Description { get; set; } = "";
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
}