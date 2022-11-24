using System.ComponentModel.DataAnnotations;

namespace Ufo.Models
{
    public class UfoType
    {
        [RegularExpression(@"^[a-zA-Z ]{1,20}$")]
        public string Type { get; set; }
    }
}
