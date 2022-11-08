using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace E_wallet.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Required]
        public int UserI { get; set; }

        [Required]
        public string CardNo { get; set; }

        [Required]
        public string Expire { set; get; }

        [Required]
        public string Cvv { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string Ifsc { get; set; }

        [Required]
        public string Holdername { get; set; }

        public Card()
        {

        }
        
    }
}
