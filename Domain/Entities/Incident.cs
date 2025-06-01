using System.ComponentModel.DataAnnotations;
using Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Utilities.Constants.Enums;


namespace Domain.Entities
{
    public class Incident : BaseModel
    {
        [Key]
        public Guid Oid { get; set; }
         
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [DataType(DataType.Text)]
        [Display(Name = "Ticket Title")]
        public string TicketTitle { get; set; }
        
        [Display(Name = "Resolved Request")]
        public bool ResolvedRequest { get; set; }
        
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date Resolved")]
        public DateTime? DateResolved { get; set; }
        
        [Display(Name = "IsResolved")]
        public bool IsResolved { get; set; }
        
        [Display(Name = "IsOpen")]
        public bool IsOpen { get; set; }
        
        public int? BranchId { get; set; }
        [ForeignKey("BranchId")]
        [JsonIgnore]
        public virtual Branch Branch  { get; set; }

        [Display(Name = "UserAccountId")]
        public Guid? UserAccountId { get; set; }
        
        [ForeignKey("UserAccountId")]
        [JsonIgnore]
        public virtual UserAccount UserAccounts { get; set; }

        public Guid? CustomerId { get; set; }
         [ForeignKey("CustomerId")]
         [JsonIgnore]
         public virtual Customer Customer { get; set; }

        [Display(Name = "IsAssign")]
        public bool IsAssigned { get; set; }
        
        public TicketPriority? IncidentPriority  { get; set; }
        
        [Display(Name = "CategoryId")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]

        [JsonIgnore]
        public virtual IncidentCategory IncidentCategory { get; set; }
        
        [JsonIgnore]
        public virtual IEnumerable<Message> Messages { get; set; }
        
        [JsonIgnore]
        public virtual IEnumerable<Screenshot> Screenshots { get; set; }
        
        public TicketType? ticketType { get; set; }
        
        public int? NetworkDeviceId { get; set; }

        [ForeignKey("NetworkDeviceId")]
        [JsonIgnore]
        public virtual NetworkDevice NetworkDevice { get; set; }
        
        [NotMapped]
        public List<string> Images { get; set; }
    }
}