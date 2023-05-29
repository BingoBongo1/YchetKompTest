using System;
using System.Collections.Generic;

namespace YchetKomp.Models
{
    public partial class User
    {
        public User()
        {
            ClaimUserIdCloseNavigations = new HashSet<Claim>();
            ClaimUserIdOpenNavigations = new HashSet<Claim>();
            Devices = new HashSet<Device>();
        }

        public int UserId { get; set; }
        public string UserLogin { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Claim> ClaimUserIdCloseNavigations { get; set; }
        public virtual ICollection<Claim> ClaimUserIdOpenNavigations { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
