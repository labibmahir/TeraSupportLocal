using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class CustomerOrganization : BaseModel
{
    [Key]
    public Guid Oid { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Address { get; set; }
    
    [Required]
    public string PrimaryPhone { get; set; }
    
    public string SecondaryPhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public string Website { get; set; }
    
    public string FacebookPageUrl { get; set; }
    
    public string LinkedInPageUrl { get; set; }
    
    [JsonIgnore]
    public virtual IEnumerable<Customer> Customers { get; set; }
}