using System.ComponentModel.DataAnnotations;

namespace Ufo.Models
{
    public class Observation
    {
        public int Id { get; set; }
        public string UfoType { get; set; }
        //[RegularExpression(@"^$")]
        public string Date { get; set; }
        //[RegularExpression(@"^$")]
        public string Time { get; set; }
        [RegularExpression(@"^[a-zA-Z .,-?!]{1,200}$")]
        public string Description { get; set; }
        [RegularExpression(@"^[0-9.-]{1,10}$")]
        public string Latitude { get; set; }
        [RegularExpression(@"^[0-9.-]{1,10}$")]
        public string Longitude { get; set; }
    }
}