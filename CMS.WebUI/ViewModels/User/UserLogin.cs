using System.ComponentModel.DataAnnotations;

namespace CMS.WebUI.ViewModels.User
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Câmpul pentru numele de utilizator trebuie completat.")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Câmpul pentru parolă trebuie completat.")]
        public string Password { get; set; }
    }
}
