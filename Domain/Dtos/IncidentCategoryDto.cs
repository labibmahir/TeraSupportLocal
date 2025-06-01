using Domain.Entities;

namespace Domain.Dtos;

public class IncidentCategoryDto
{
    public int CategoryId { get; set; }
    public string IncidentCategorys { get; set; } = string.Empty;
    public int ParentId { get; set; }
    public DateTime? DateCreated { get; set; }
    public Guid? CreatedBy { get; set; }
    public string Description { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DateModified { get; set; }

    public IncidentCategory ParentCategory { get; set; } = new IncidentCategory();

}