using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ProductCategory : BaseModel
{
    [Key]
    public int Oid { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [JsonIgnore]
    public virtual IEnumerable<Product> Products { get; set; }
}