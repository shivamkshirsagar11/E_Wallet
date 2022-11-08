using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace E_wallet.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        [Required]
        public int UserI { get; set; }

        [Required]
        public int BankI{ get; set; }

        [Required]
        public string LastOperated { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public string TakeANote { get; set; }

        public Wallet() {
            this.BankI = -1;
            this.UserI = -1;
            this.LastOperated = "Never Used";
            this.Balance = 0.0;
            this.TakeANote = "Wallet is Not Ready to Use";
        }

    }
}
