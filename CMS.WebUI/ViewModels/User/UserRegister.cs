using System.ComponentModel.DataAnnotations;

namespace CMS.WebUI.ViewModels.User
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Email cannot be null!")]
        [EmailAddress(ErrorMessage = "This is not a valid email address!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username cannot be null!")]
        public string UserName { get; set; }
        public string CompleteName { get; set; }
        [Required(ErrorMessage = "Password cannot be null!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The two passwords do not match!")]
        public string ConfirmPassword { get; set; }
        public Ranks Rank { get; set; }
    }

    public enum Ranks
    {
        Student,
        Professor,
        Admin
    }
}
