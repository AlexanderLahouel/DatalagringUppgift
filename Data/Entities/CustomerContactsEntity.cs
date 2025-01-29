using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Data.Entities;
public class CustomerContacts
{
    [Key, Column(Order = 1)]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Key, Column(Order = 2)]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Key, Column(Order = 3)]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; } 
    public Customer Customer { get; set; } = null!;
}
