using System.ComponentModel.DataAnnotations;

public class UnitType
{
    [Key]
    public int Id { get; set; }  

    [Required]
    [MaxLength(50)]
    public string UnitName { get; set; } = string.Empty;  // Example: "Hour", "Day", "Project"


    public ICollection<Service> Services { get; set; } = new List<Service>();
}
