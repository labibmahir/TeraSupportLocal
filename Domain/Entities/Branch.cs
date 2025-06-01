using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Utilities.Constants;

namespace Domain.Entities;

public class Branch : BaseModel
{       /// <summary>
        /// Primary Key of the table Facility.
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Name of the Facility.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(90)]
        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        /// <summary>
        /// Address of the Facility.
        /// </summary>
        [StringLength(255) ]
        [Display(Name = "Facility Address")]
        public string Address { get; set; }
        
        /// <summary>
        /// Is Active of the Branch.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "Is Active")]
        public bool IsActive  { get; set; }

        /// <summary>
        /// Opening Date of the Branch.
        /// </summary>
        [Display(Name = "Branch Opening Date")]
        public DateTime?  OpeningDate { get; set; }
        
        /// <summary>
        /// Closing Date of the Branch.
        /// </summary>
        [Display(Name = "Branch Closing Date")]
        public DateTime?  ClosingDate { get; set; }
        
        /// <summary>
        /// Incidents of a Facility.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Incident> Incidents { get; set; }

        /// <summary>
        /// UserAccounts of a Role.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<BranchPermission> BranchPermissions { get; set; }

        /// <summary>
        /// This field in not insert.
        /// </summary>
        [NotMapped]
        public int ProvinceId { get; set; }

        /// <summary>
        /// This field in not insert.
        /// </summary>
        [NotMapped]
        public int CountryId { get; set; }
    }