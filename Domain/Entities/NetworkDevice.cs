using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Utilities.Constants.Enums;

namespace Domain.Entities
{
    public class NetworkDevice
    {
         /// <summary>
         /// Primary key of the table NetwordDevice.
         /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Name of the Network Device.
        /// </summary>
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Display(Name = "Device Name")]
        public string DeviceName { get; set; }

        /// <summary>
        /// IP Address of the Network Device.
        /// </summary>
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Required]
        [Display(Name = "Device Type")]
        public NetworkDeviceType DeviceType { get; set; }

        public SNMPTracker SNMPTracker { get; set; }

        public string CommunityString { get; set; }

        public SnmpVersion snmpVersion { get; set; }
        
        /// <summary>
        /// Email of the Device Admin.
        /// </summary>
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Device Admin Email")]
        [EmailAddress]
        public string DeviceAdminEmailAdress { get; set; }
        
        /// <summary>
        /// Foreign Key. Primary key of the entity Facility.
        /// </summary>
        public int? BranchId { get; set; }
        
        [ForeignKey("BranchId")]
        [JsonIgnore]
        public virtual Branch Branch  { get; set; }
        
        [JsonIgnore]
        public virtual IEnumerable<MonitorNetworkDeviceConfiguration> MonitorNetworkDeviceConfigurations { get; set; }
    }
}