using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class DeviceType
    {
        public DeviceType()
        {
            Devices = new HashSet<Device>();
        }

        public int DeviceTypeId { get; set; }
        public string DeviceTypeName { get; set; } = null!;

        public virtual ICollection<Device> Devices { get; set; }
    }
}
