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


        public DataTable obtenerTabla(string consulta)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(consulta, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
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
