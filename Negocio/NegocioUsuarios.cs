using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioUsuarios
    {

        private DaoUsuarios dao = new DaoUsuarios();

        public DataTable getTabla(string usuario, string contrasenia)
        {
            return dao.getTablaUsuario(usuario, contrasenia);
        }

        public bool buscarUsuario(string usuario, string contrasenia)
        {
            DataTable dt = getTabla(usuario, contrasenia);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable verificarCredenciales(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            return dao.buscarUsuarioConRol(username, password);
        }

        public string getRolUsuario(string usuario, string password)
        {
            DataTable dt = getTabla(usuario, password);

            DataRow row = dt.Rows[0];

            string Rol = (string)row["Rol"];

            if(Rol == "M") 
            {
                return "Medico";
            }
            else if ( Rol == "A")
            {
                return "Admin";
            }
            else
            {
                return null;
            }
        }

    }
}
