using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ProductDetail : BaseModel
{
    [Key]
    public Guid Oid { get; set; }
    
    [Required]
    public decimal UnitPrice { get; set; }
    
    public decimal? Tax { get; set; }
    
    public decimal? Discount { get; set; }
   
    [Required]
    public bool IsActive { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    [JsonIgnore]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}