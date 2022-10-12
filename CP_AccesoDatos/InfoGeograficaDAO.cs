using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CP_Entidades;

namespace CP_AccesoDatos
{
    //Provincia - Canton - Distrito
    public class ProvinciaDAO
    {

        #region "Constructor de Provincia"
        private static ProvinciaDAO Provincia = null;
        private ProvinciaDAO() { }
        public static ProvinciaDAO getInstancia()
        {
            if (Provincia == null)
            {
                Provincia = new ProvinciaDAO();
            }
            return Provincia;
        }
        #endregion

        //Get Provincias
        public List<Provincia> GetProvincias()
        {
            List<Provincia> lista = new List<Provincia>();
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;

            conexion = ConexionDAO.getInstancia().ConexionBD();
            cmd = new SqlCommand("dbo.sp_web_geo_provincia", conexion);
            cmd.CommandType = CommandType.StoredProcedure;


            conexion.Open();
            dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                Provincia ObjProvincia = new Provincia();

                ObjProvincia.cod_provincia = dr["Id_Provincia"].ToString();
                ObjProvincia.nombre = dr["Nombre"].ToString();
                //Añadir a la lista
                lista.Add(ObjProvincia);
            }

            return lista;
            //}
        }

    }

    public class CantonDAO
    {

        #region "Constructor de Canton"
        private static CantonDAO Canton = null;
        private CantonDAO() { }
        public static CantonDAO getInstancia()
        {
            if (Canton == null)
            {
                Canton = new CantonDAO();
            }
            return Canton;
        }
        #endregion

        //Get Cantones
        public List<Canton> GetCantones(string provinciaId)
        {
            List<Canton> lista = new List<Canton>();
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;


            String cod_provincia = provinciaId;

            try
            {
                //Instancia de Conexión
                conexion = ConexionDAO.getInstancia().ConexionBD();

                cmd = new SqlCommand("dbo.sp_web_geo_canton", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProvincia", cod_provincia);

                conexion.Open();


                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Canton ObjCanton = new Canton();

                    ObjCanton.cod_provincia = dr["Id_Provincia"].ToString();
                    ObjCanton.cod_canton = dr["Id_Canton"].ToString();
                    ObjCanton.nombre = dr["Nombre"].ToString();
                    //Añadir a la lista
                    lista.Add(ObjCanton);
                }
            }
            catch (Exception ex)
            {
                lista = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }
    }

    public class DistritoDAO
    {
        #region "Constructor de Distrito"
        private static DistritoDAO Distrito = null;
        private DistritoDAO() { }
        public static DistritoDAO getInstancia()
        {
            if (Distrito == null)
            {
                Distrito = new DistritoDAO();
            }
            return Distrito;
        }
        #endregion

        //Get Distritoes
        public List<Distrito> GetDistritos(string provinciaId, string cantonId)
        {
            List<Distrito> lista = new List<Distrito>();
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;

            String cod_provincia = provinciaId;
            String cod_canton = cantonId;

            try
            {
                conexion = ConexionDAO.getInstancia().ConexionBD();
                //Tipo y nombre del proceso a ejecutar
                cmd = new SqlCommand("dbo.sp_web_geo_distrito", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProvincia", cod_provincia);
                cmd.Parameters.AddWithValue("@IdCanton", cod_canton);

                conexion.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Distrito ObjDistrito = new Distrito();

                    ObjDistrito.cod_distrito = dr["Codigo"].ToString();
                    ObjDistrito.cod_canton = dr["Id_Canton"].ToString();
                    ObjDistrito.nombre = dr["Nombre"].ToString();
                    //Añadir a la lista
                    lista.Add(ObjDistrito);
                }
            }
            catch (Exception ex)
            {
                lista = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }
    }
}