using System.ComponentModel.DataAnnotations;
using Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Dynamic;

namespace Domain.Entities
{
    public class Message : BaseModel
    {
        /// <summary>
        /// Primary key of the table Message.
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Message Date date of the row.
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Message Date")]
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Name of the Message.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        public string Messages { get; set; }

        public Guid IncidentId { get; set; }

        [ForeignKey("IncidentId")]
        [JsonIgnore]

        public virtual Incident Incident { get; set; }


        /// <summary>
        /// IsOpen is a status of the Message.
        /// </summary>
        public bool IsOpen { get; set; }
    }
}