using System.ComponentModel.DataAnnotations;

namespace Authorization.DbModels
{
    /// <summary>
    /// Модель данных для регистрации пользователя
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Создает модель данных регистрации
        /// </summary>
        /// <param name="userName">логин пользователя</param>
        /// <param name="email">email пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <param name="confirmPassword"> пароль для подтверждения</param>
        /// <param name="firstName">имя пользователя</param>
        /// <param name="lastName"> фамиля пользователя</param>
        /// <param name="city">город пользователя</param>
        /// <param name="phoneNumber"></param>
        public RegisterModel(string userName, string email, string password, string confirmPassword, string firstName, string lastName, string city, string phoneNumber)
        {
            UserName = userName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Пароль для подтверждения
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Имф пользователя
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Город пользователя
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        /// <value></value>
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; }
    }
}
