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

    }
}
