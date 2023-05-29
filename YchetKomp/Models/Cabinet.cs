using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class Cabinet
    {
        public Cabinet()
        {
            Devices = new HashSet<Device>();
        }

        public int CabinetId { get; set; }
        public string Cabinet1 { get; set; } = null!;
        public int CorpsId { get; set; }

        public virtual Corps Corps { get; set; } = null!;
        public virtual ICollection<Device> Devices { get; set; }
    }
}
