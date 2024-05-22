using System.ComponentModel.DataAnnotations;

namespace MVC_Agency.DTOs.AccountDto
{
    public class LoginDto
    {
        
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
