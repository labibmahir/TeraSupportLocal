using System.ComponentModel.DataAnnotations;

namespace Utilities.Constants;

public static class Enums
{
    public enum Gender : byte
    {
        Male = 1,
        
        Female = 2,
        
        Other = 3
    }
    public enum SnmpVersion
    {
            Ver1 = 1,
            Ver2 = 2,
            Ver3 = 3
     }

        public enum TicketType : byte
        {
            Hardware = 1,
            Software = 2,
            Network = 3,
            Other = 4
        }
         public enum NetworkDeviceType : byte
        {
            Router = 1,
            Switch = 2,
        }
        public enum SNMPTracker : byte
        {
            [Display(Name = "Enable")]
            Enable = 1,

            [Display(Name = "Disable")]
            Disable = 2,

            [Display(Name = "Not Applicable")]
            NotApplicable = 3
        }
        public enum TicketPriority : byte
        {
            [Display(Name = "Critical")]
            Critical = 1,

            [Display(Name = "High")]
            High = 2,

            [Display(Name = "Medium")]
            Medium = 3,

            [Display(Name = "Low")]
            Low = 4,

            [Display(Name = "Very Low")]
            VeryLow = 5,

            [Display(Name = "Not Applicable")]
            NotApplicable = 6
        }
        
        public enum StageStatus : byte
        {
            Initial = 1,
            
            OnGoing = 2,
            
            Completed = 3,
            
            Failed = 4,
        }
}