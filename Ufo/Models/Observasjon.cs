namespace Ufo.Models
{
    public class Observasjon
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Dato { get; set; }
        public string Tid { get; set; }
        public string Beskrivelse { get; set; }
        public string Lokasjon { get; set; }
    }
}