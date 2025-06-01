using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class DealTemplate : BaseModel
{
    [Key]
    public int Oid { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [JsonIgnore]
    public virtual IEnumerable<DealStage>DealStages { get; set; }
}