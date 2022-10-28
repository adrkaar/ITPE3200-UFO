namespace Ufo.Models
{
    public class Observation
    {
        public int Id { get; set; }
        public string UfoType { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}