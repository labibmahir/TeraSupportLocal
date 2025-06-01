using System.ComponentModel.DataAnnotations;
using Utilities.Constants;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class IncidentCategory : BaseModel
    {
        /// <summary>
        /// Primary key of the table IncidentCategory.
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Name of the IncidentCategory.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Purpose of the IncidentCategory.
        /// </summary>        
        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public int TeamId { get; set; }
        [ForeignKey("TeamId")]

        [JsonIgnore]
        public virtual Team Team { get; set; }

        /// <summary>
        /// Incident of a IncidentCategory.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Incident> Incidents { get; set; } 
    }
}