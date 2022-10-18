using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ufo.Models
{
    public class Observasjonlokasjon
    {
        public int Id  {get; set; }                   // FK fra Observasjon
        public string Gps {get; set;}
    }
}
