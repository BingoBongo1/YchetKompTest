using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class Device
    {
        public Device()
        {
            Claims = new HashSet<Claim>();
            Histories = new HashSet<History>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ManufacturerId { get; set; }
        public int DeviceTypeId { get; set; }
        public string InventoryNumber { get; set; } = null!;
        public string NameDevice { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? Decommissioned { get; set; }
        public int CabinetId { get; set; }

        public virtual Cabinet Cabinet { get; set; } = null!;
        public virtual DeviceType DeviceType { get; set; } = null!;
        public virtual Manufacturer Manufacturer { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<History> Histories { get; set; }
    }
}
