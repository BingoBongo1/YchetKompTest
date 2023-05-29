using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class StatusClaim
    {
        public StatusClaim()
        {
            Claims = new HashSet<Claim>();
        }

        public int StatusClaimId { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Claim> Claims { get; set; }
    }
}
