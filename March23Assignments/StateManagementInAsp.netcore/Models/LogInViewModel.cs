using System.ComponentModel.DataAnnotations;

namespace StateManagementInAsp.netcore.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
