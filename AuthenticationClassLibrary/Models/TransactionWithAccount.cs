using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationClassLibrary.Models
{
    public class TransactionWithAccount
    {
        public int TransactionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        public long Source_acc { get; set; }
        public long Dest_acc { get; set; }
        public Account AccountInfo { get; set; } // Navigation property to include account details

        public TransactionWithAccount()
        {
            AccountInfo = new Account(); // Initialize the account info object
        }
    }
}
