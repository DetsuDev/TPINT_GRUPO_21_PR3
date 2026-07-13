using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public Persona persona;
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Rol { get; set; } 
        public bool Estado { get; set; }

        public Usuario()
        {
            persona = new Persona();
        }
    }
}
