using System.ComponentModel.DataAnnotations;

namespace Bilet1.ViewModels.Account
{
    public class LoginVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool IsPersisted { get; set; }
    }
}
