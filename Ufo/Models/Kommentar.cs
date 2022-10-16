using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Models
{
    public class Kommentar
    {
        public int Id  {get; set;}
        public string Kommentar {get; set;}
        
        // FK Person: person
        // FK Observasjon: observasjon
}
}
