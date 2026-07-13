using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico : Persona
    {
        public int IdMedico { get; set; }
        public string LegajoMedico { get; set; }
        public int IdEspecialidad { get; set; }
        public bool Estado { get; set; }

        public string Usuario { get; set; }
        public string Contrasenia { get; set; }

        public string DiasDisponibles { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }

    }
}
