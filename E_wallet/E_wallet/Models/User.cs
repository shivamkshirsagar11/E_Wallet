using System.ComponentModel.DataAnnotations;

namespace E_wallet.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Mobile { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Use { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Zipcode { get; set; }
        public User() { }

        public bool ValidateEmptyOrBasicError()
        {
            bool ret = false;
            if((Use == null) || (Mobile == null) || (Address == null) || (Email == null) || (Password == null) || (Name == null) || (Gender == null) || (Zipcode == null))
            {
                ret = true;
            }
            return ret;
        }

    }
}