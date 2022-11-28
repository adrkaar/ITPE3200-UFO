using System.ComponentModel.DataAnnotations;

namespace Ufo.Models
{
    public class User
    {
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Username { get; set; }
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }
    }
}
