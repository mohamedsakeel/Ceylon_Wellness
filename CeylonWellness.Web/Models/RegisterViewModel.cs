using System.ComponentModel.DataAnnotations;

namespace CeylonWellness.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(1, 31, ErrorMessage = "Please select a valid day.")]
        public int Day { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Please select a valid month.")]
        public int Month { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Please enter a valid year.")]
        public int Year { get; set; }

        [Display(Name = "Date of Birth")]
        public DateOnly DateOfBirth => new DateOnly(Year, Month, Day);

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
    }
}
