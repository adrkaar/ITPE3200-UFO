using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Models
{
    public class Rating
    {
        public int Id  {get; set;}
        public double Skala {get; set;}
        // FK Person: person
        // FK Observasjon: observasjon
}
}
