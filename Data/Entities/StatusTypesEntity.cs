using Data.Entities;
using System.ComponentModel.DataAnnotations;

public class StatusType
{
    [Key]
    public int Id { get; set; } 
    public string Status { get; set; } = string.Empty; 

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
