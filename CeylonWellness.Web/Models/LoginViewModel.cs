using System.ComponentModel.DataAnnotations;

namespace CeylonWellness.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string username { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
