using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Utilities.Constants;

namespace Domain.Entities
{
    public class Screenshot : BaseModel
    {
        /// <summary>
        /// Primary key of the table Screenshot.
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// User Ticket Attachment
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        public Byte[] Screenshots { get; set; }

        public Guid IncidentId { get; set; }
        /// <summary>
        /// Foreign key. Primary key of the entity Incident. 
        /// </summary>
        [ForeignKey("IncidentId")]
        [JsonIgnore]
        public virtual Incident Incidents { get; set; }
    }
}