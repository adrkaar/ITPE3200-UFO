using System.ComponentModel.DataAnnotations;

namespace Ufo.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z .,-?!]{1,200}$")]
        public string Text { get; set; }
        public int ObservationId { get; set; }
        [RegularExpression(@"^[0-9]{1,3}$")]
        public int UpVote { get; set; }
        [RegularExpression(@"^[0-9]{1,3}$")]
        public int DownVote { get; set; }
    }
}