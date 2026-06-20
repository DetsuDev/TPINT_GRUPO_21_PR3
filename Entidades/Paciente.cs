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
        public int IdPaciente { set; get; }
        public bool Estado { set; get; }

        ///  El IdPersona ya se hereda de la clase Persona

    }
}
