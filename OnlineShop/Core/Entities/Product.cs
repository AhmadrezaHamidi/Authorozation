using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// [Table("MyProducts", Schema ="Base")]
public class Product
{
    // [Key]
    public int Id { get; set; }
    // [MaxLength(128), Required]
    public string ProductName { get; set; }
    public long Price { get; set; }
}