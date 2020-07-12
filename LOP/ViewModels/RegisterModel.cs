using System.ComponentModel.DataAnnotations;

namespace LOP.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       public int Role { get; set; }
                
    }
    
}
