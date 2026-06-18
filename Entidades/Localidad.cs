using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public int IdProvincia { get; set; }
        public string NombreLocalidad { get; set; }

        public Localidad() { }
    }
}
