using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class TeamLead :  BaseModel
{
   [Key]
    public int  Oid { get; set; }   
    
    public Guid UserAccountId { get; set; }
    [ForeignKey("UserAccountId")]
    
    [JsonIgnore]
    public virtual UserAccount UserAccount { get; set; } = new UserAccount();
    
    public int TeamId { get; set; }
    [ForeignKey("TeamId")]
    
    [JsonIgnore]
    public virtual Team Team { get; set; } = new Team();
}