using System.ComponentModel.DataAnnotations;

namespace Ufo.Models
{
    public class Observation
    {
        public int Id { get; set; }
        public string UfoType { get; set; }
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
        public string Date { get; set; }
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$")]
        public string Time { get; set; }
        [RegularExpression(@"^[a-zA-Z .,-?!]{1,200}$")]
        public string Description { get; set; }
        [RegularExpression(@"^[0-9.-]{1,10}$")]
        public string Latitude { get; set; }
        [RegularExpression(@"^[0-9.-]{1,10}$")]
        public string Longitude { get; set; }
    }
}