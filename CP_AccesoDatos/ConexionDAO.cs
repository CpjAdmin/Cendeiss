using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CP_AccesoDatos
{
    public class ConexionDAO
    {

        #region "Constructor de Asociado"
        private static ConexionDAO Conexion = null;
        private ConexionDAO() { }
        public static ConexionDAO getInstancia()
        {
            if (Conexion == null)
            {
                Conexion = new ConexionDAO();
            }
            return Conexion;
        }
        #endregion

        public SqlConnection ConexionBD()
        {
            try
            {
                string conexionString = ConfigurationManager.ConnectionStrings["ConexionCendeisss"].ConnectionString;

                SqlConnection conexion = new SqlConnection(conexionString);
                return conexion;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al conectar SqlConnection ", ex);
            }
        }
    }
}
