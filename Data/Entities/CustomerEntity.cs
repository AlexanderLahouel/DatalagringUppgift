using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    [Required]
    public string CustomerName { get; set; } = null!;


    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<CustomerContacts> CustomerContacts { get; set; } = new List<CustomerContacts>();

}
