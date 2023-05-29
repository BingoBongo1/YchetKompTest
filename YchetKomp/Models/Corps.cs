using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class Corps
    {
        public Corps()
        {
            Cabinets = new HashSet<Cabinet>();
        }

        public int CorpsId { get; set; }
        public string CorpsName { get; set; } = null!;

        public virtual ICollection<Cabinet> Cabinets { get; set; }
    }
}
