using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_wallet.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required][EmailAddress]
        public string Email { get; set; }
        [Required][Phone]
        public string Mobile { get; set; }
    }
}