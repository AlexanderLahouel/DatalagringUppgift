using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    public string ProjectNumber { get; set; } = $"P-{Guid.NewGuid().ToString().Substring(0, 4)}";
    public string Name { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = "Not started";
    public string ProjectManager { get; set; } = null!;
   
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
