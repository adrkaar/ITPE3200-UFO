using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Models
{
    public class Observasjon
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Dato { get; set; }
        public string Tid {get; set;}
        public string Beskrivelse {get; set;}
        // FK lokasjon: observasjonslokasjon
        // FK UFO: UFO

}
}
