using System.ComponentModel.DataAnnotations;

namespace Bilet1.ViewModels.Account
{
    public class RegisterVM
    {
        [MinLength(3,ErrorMessage ="Qisadir")]
        [MaxLength(40,ErrorMessage ="Uzundur")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "Qisadir")]
        [MaxLength(40, ErrorMessage = "Uzundur")]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string  ConfirmPassword { get; set; }
    }
}
