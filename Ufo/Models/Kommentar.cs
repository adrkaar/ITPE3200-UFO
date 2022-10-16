using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Models
{
    public class Kommentar
    {
        public int Id  {get; set;}
        public string Komment {get; set;}         // Variable cannot have the same name as class. So, I changed "Kommentar" to "Komment"
        
        // FK Person: person
        // FK Observasjon: observasjon
}
}
