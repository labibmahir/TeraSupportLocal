using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Utilities.Constants.Enums;

namespace Domain.Entities;

public class UserAccount : BaseModel
{
    [Key]
    public Guid Oid { get; set; }
    
    [Required(ErrorMessage = "Firstname is required")]
    [StringLength(90,ErrorMessage = "Invalid Length")]
    public string FirstName { get; set; }
    
    [StringLength(90,ErrorMessage = "Invalid Length")]
    public string Surname { get; set; }
    
    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }
    
    [Required(ErrorMessage = "Birth date is required")]
    public DateTime DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [StringLength(250, ErrorMessage = "Invalid Length")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Country Code is required")]
    [StringLength(10, ErrorMessage = "Invalid Length")]
    public string CountryCode { get; set; }
    
    [Required(ErrorMessage = "Cellphone is required")]
    [StringLength(20, ErrorMessage = "Invalid Length")]
    public string Cellphone { get; set; }
    
    [StringLength(500, ErrorMessage = "Invalid Length")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Invalid Length")]
    public string Password { get; set; }
    
    [JsonIgnore]
    public virtual IEnumerable<Biometric> Biometrics { get; set; }
}