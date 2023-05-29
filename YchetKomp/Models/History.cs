using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class History
    {
        public int HistoryId { get; set; }
        public int DeviceId { get; set; }
        public string OldInventoryNumber { get; set; } = null!;
        public DateTime Date { get; set; }

        public virtual Device Device { get; set; } = null!;
    }
}
