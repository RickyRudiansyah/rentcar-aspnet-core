using System.ComponentModel.DataAnnotations;

namespace RentCar.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username wajib diisi")]
        [Display(Name = "Username")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password wajib diisi")]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}