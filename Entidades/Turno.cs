using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public int IdEstado { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } 
        public string Observacion { get; set; }

        public Turno() { }
    }
}
