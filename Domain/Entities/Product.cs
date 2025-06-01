using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Product : BaseModel
{
    [Key]
    public Guid Oid { get; set; }
    
    [Required]
    public string ProductName { get; set; }
    
    [Required]
    public string ProductCode { get; set; }
    
    public string Description { get; set; }
    
    [Required]
    public int ProductCategoryId { get; set; }
    
    [JsonIgnore]
    [ForeignKey("ProductCategoryId")]
    public virtual ProductCategory ProductCategory { get; set; }
    
    [JsonIgnore]
    public virtual IEnumerable<ProductDetail> ProductDetails { get; set; }
}