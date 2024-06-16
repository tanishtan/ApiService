using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationClassLibrary.Models
{
    public class Account
    {
        public long? AccountID { get; set; }
        public decimal? Balance { get; set; }
        public bool? hasCheque { get; set; }
        public int? wd_quota { get; set; }
        public int? dp_quota { get; set; }
        public bool? isActive { get; set; }
        public int? CustomerID { get; set; }
        public int? type_id { get; set; }
        public string? BranchID { get; set; }
    }
}
