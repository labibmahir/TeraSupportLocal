using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Customer : BaseModel
{
    [Key]
    public Guid Oid { get; set; }
    
    [Required]
    public string FullName { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    [Required]
    public Guid? CustomerOrganizationId { get; set; }
    
    [JsonIgnore]
    [ForeignKey("CustomerOrganizationId")]
    public virtual CustomerOrganization CustomerOrganization { get; set; }
}