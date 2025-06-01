using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Biometric : BaseModel
{
    [Key]
    [ForeignKey("UserAccount")]
    public Guid Oid { get; set; }
    
    public byte[]? Image { get; set; }
    
    public string Fingerprint { get; set; }
    
    [JsonIgnore]
    public virtual UserAccount UserAccount { get; set; }
}