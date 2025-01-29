using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;


    public ICollection<CustomerContacts> Contacts { get; set; } = new List<CustomerContacts>();

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
