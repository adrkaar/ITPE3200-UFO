namespace Ufo.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ObservationId { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public string Username { get; set; }
    }
}
