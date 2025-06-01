using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Utilities.Constants;

namespace Domain.Entities;

public class Team : BaseModel
{
    [Key]
    public int Oid { get; set; }

    [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
    [StringLength(90)]
    [Display(Name = "Team Name")]
    public string Name { get; set; }

    [JsonIgnore]
    public virtual IEnumerable<Incident> Incidents { get; set; }
} 