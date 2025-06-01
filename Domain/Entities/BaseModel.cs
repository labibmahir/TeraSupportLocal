using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BaseModel
{
    public Guid? CreatedBy { get; set; }
    
    public DateTime? DateCreated { get; set; } = DateTime.Now;
    
    public Guid? ModifiedBy { get; set; }
    
    public DateTime? DateModified { get; set; }
    
    public Guid? OrganizationId { get; set; }
    
    public bool? IsDeleted { get; set; } = false;

    public bool? IsSynced { get; set; } = false;
    
    public bool? IsTPSynced { get; set; } = false;
}