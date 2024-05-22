using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MVC_Agency.DTOs.AccountDto
{
    public class RegisterDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
