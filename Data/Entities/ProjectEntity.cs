using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }

    public string ProjectNumber { get; set; } = null!;
    public string Name { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    public bool IsValidPeriod => EndDate > StartDate;
    public int ProjectManagerId { get; set; }
    public Employee ProjectManager { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int ServiceId { get; set; } 
    public Service Service { get; set; } = null!;

    public int StatusTypeId { get; set; }
    public StatusType StatusType { get; set; } = null!;
}

