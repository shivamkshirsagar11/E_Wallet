using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace E_wallet.Models
{
    public class Passbook
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public int UserI { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Action { get; set; }

        public Passbook() { }
    }
}
