using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Service
{
    [Key]
    public int Id { get; set; }  


    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    [ForeignKey("UnitType")]
    public int UnitTypeId { get; set; }  


    public UnitType UnitType { get; set; } = null!;
}
