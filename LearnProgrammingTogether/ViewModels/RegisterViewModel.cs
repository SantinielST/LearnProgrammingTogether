using System.ComponentModel.DataAnnotations;

namespace LearnProgrammingTogether.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email адрес")]
        [Required(ErrorMessage = "Требуется ввести email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Тебуется ввести пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string Password { get; set; }

        [Display(Name = "Подтвердить пароль")]
        [Required(ErrorMessage = "Тебуется подтвердить пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Тебуется ввести имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Никнейм")]
        [Required(ErrorMessage = "Тебуется ввести никнейм")]
        public string Nickname { get; set; }

        [Display(Name = "Изучаемый язык")]
        [Required(ErrorMessage = "Тебуется ввести изучаемый язык")]
        public string StudyLang { get; set; }

        [Display(Name = "Фреймворк")]
        [Required(ErrorMessage = "Тебуется ввести фреймворк")]
        public string TypeFramework { get; set; }

        [Display(Name = "Уровень владения")]
        [Required(ErrorMessage = "Тебуется ввести уровень владения")]
        public string Level { get; set; }
    }
}
