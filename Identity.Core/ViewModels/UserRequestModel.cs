using System.ComponentModel.DataAnnotations;

namespace Identity.Core.ViewModels
{
    public class UserRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}