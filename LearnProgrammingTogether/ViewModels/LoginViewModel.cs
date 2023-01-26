using System.ComponentModel.DataAnnotations;

namespace LearnProgrammingTogether.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email адрес")]
        [Required(ErrorMessage = "Введите email для входа!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль для входа!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
