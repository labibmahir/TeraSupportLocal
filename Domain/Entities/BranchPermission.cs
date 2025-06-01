using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BranchPermission : BaseModel
    {
        [Key]   
        public int Oid { get; set; }
        
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
        
        public Guid  UserAccountId { get; set; }
        [ForeignKey("UserAccountId")]
        public virtual UserAccount UserAccount { get; set; }
        
        public bool IsActive { get; set; } 
        
    }
}