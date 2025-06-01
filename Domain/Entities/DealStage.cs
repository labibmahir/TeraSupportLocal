using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Utilities.Constants;

namespace Domain.Entities;

public class DealStage : BaseModel
{
    [Key]
    public Guid Oid { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int StageSerial { get; set; }
    
    [Required]
    public Enums.StageStatus Status { get; set; }
    
    [Required]
    public int DealTemplateId { get; set; }
    
    [JsonIgnore]
    [ForeignKey("DealTemplateId")]
    public virtual DealTemplate DealTemplate { get; set; }
}