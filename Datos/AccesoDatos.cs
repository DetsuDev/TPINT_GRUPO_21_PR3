using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoDatos
    {
        
        private const string cadenaConexion = "Data Source = localhost\\SQLEXPRESS;Initial Catalog = ClinicaDB; Integrated Security = True";

        private SqlDataAdapter ObtenerAdaptador(String consultaSql, SqlConnection cn)
        {
            SqlDataAdapter adaptador;
            try
            {
                adaptador = new SqlDataAdapter(consultaSql, cn);
                return adaptador;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable obtenerTabla(string consulta)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = ObtenerAdaptador(consulta, sqlConnection);

            if (sqlDataAdapter != null)
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
                return dataTable;
            }
            else
            {
                sqlConnection.Close();
                return null;
            }  
        }

        public void ejecutarConsulta(string consulta)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

    }
}
