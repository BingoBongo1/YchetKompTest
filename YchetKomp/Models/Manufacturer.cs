using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Devices = new HashSet<Device>();
        }

        public int ManufacturerId { get; set; }
        public string Manufacturer1 { get; set; } = null!;

        public virtual ICollection<Device> Devices { get; set; }
    }
}
