using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Website.Models;


public class Product
{
    [Key]
    [DisplayName("Id")]
    public int ProductId { get; set; }
    [Required]
    [DisplayName("Nom")]
    public string ProductName { get; set; }
    [Required]
    [DisplayName("Référence")]
    public string ProductReference { get; set; }
    [Required]
    [DisplayName("Prix")]
    [Range(0.0, Double.MaxValue, ErrorMessage = "Price must be positive")]
    public double ProductPrice { get; set; }
    [Required]
    [DisplayName("Stock")]
    [Range(0, 100000, ErrorMessage = "Stock must be positive and inferior to 100000")]
    public int productQuantity { get; set; }
    [Required]
    [DisplayName("Photo")]
    public string productPicture { get; set; }
    [Required]
    [DisplayName("Catégorie")]
    public Category Category { get; set; }

}
