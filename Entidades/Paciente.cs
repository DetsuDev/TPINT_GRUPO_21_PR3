using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paciente : Persona
    {
        public Paciente() { }
        public Paciente(int IdPaciente, bool Estado) { IdPaciente = _IdPaciente; Estado = _Estado; }
        public int _IdPaciente { set; get; }
        public bool _Estado { set; get; }

        ///  El IdPersona ya se hereda de la clase Persona

    }
}
