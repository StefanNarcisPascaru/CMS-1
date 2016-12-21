﻿using System.ComponentModel.DataAnnotations;

namespace CMS.WebUI.ViewModels
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Câmpul pentru numele de utilizator trebuie completat.")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Câmpul pentru parolă trebuie completat.")]
        public string Password { get; set; }
        [Display(Name="Rămâi conectat!")]
        public bool RememberMe { get; set; }
    }
}
