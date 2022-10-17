using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Models
{
    public class Observasjon    {
        public int Id { get; set; }                              
        public string Dato { get; set; }
        public string Tid {get; set;}
        public string Beskrivelse {get; set;}

        public string IdUfo { get; set; }                               // FK fra UFO: only IdUfo and NavnUfo will be shown in Observasjon-table
        public string NavnUfo { get; set; }

        
        public string Gsp {get; set;}                                   // FK lokasjon fra Observasjonslokasjon                       
        public object UFO { get; internal set; }
    }
}