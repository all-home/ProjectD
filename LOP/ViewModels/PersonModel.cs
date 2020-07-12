using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LOP.ViewModels
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Метка не считана")]
        public int TagId { get; set; }
        [Required(ErrorMessage = "Не указано Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Не указано Отчество")]
        public string Patronymic { get; set; }
        public string Tel { get; set; }
        public string Position { get; set; }
        public IFormFile Image { get; set; }
    }
}
