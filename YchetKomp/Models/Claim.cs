using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class Claim
    {
        public int ClaimsId { get; set; }
        public int StatusClaimId { get; set; }//
        public int DeviceId { get; set; }//
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public string Description { get; set; } = null!;    
        public int UserIdOpen { get; set; }//
        public int UserIdClose { get; set; }
        
        public virtual Device Device { get; set; } = null!;//
        public virtual StatusClaim StatusClaim { get; set; } = null!;//
        public virtual User UserIdCloseNavigation { get; set; } = null!;//
        public virtual User UserIdOpenNavigation { get; set; } = null!;//
    }
}
