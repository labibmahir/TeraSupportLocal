using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Domain.Entities
{
    public class MonitorNetworkDeviceConfiguration : BaseModel
    {
        /// <summary>
        /// Primary key of the table MonitoryNetworkDeviceConfiguration.
        /// </summary>
        [Key]
        public int Oid { get; set; }

        public int? MaxCpuUSage { get; set; }
        
        public int? MaxCpuUSageForEmailAlert { get; set; }
        
        public bool CreateTicketIfPingFailed { get; set; } = true;
        
        public bool CreateTicketIfSNMPConnectionFailed { get; set; } = true;
        
        public bool MonitorDeviceIfSNMPNotEnabled { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the entity NetworkDevice. 
        /// </summary>
        public int NetworkDeviceId { get; set; }

        [ForeignKey("NetworkDeviceId")]
        [JsonIgnore]
        public virtual NetworkDevice NetworkDevice { get; set; }
    }
}