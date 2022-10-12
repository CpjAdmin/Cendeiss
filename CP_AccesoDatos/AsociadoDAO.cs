using CP_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace CP_AccesoDatos
{
    public class AsociadoDAO
    {

        #region "Constructor de Asociado"
        private static AsociadoDAO Asociado = null;
        private AsociadoDAO() { }
        public static AsociadoDAO getInstancia()
        {
            if (Asociado == null)
            {
                Asociado = new AsociadoDAO();
            }
            return Asociado;
        }
        #endregion

        //AccesoSistema
        public Asociado AccesoSistema(String login, String clave)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Asociado ObjAsociado = null;

            try
            {
                conexion = ConexionDAO.getInstancia().ConexionBD();

                cmd = new SqlCommand("dbo.sp_web_autenticacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@clave", clave);

                //Pendiente logica para controlar error de login
                //SqlParameter validacion = cmd.Parameters.Add("@validacion", SqlDbType.Int);
                //validacion.Direction = ParameterDirection.Output;

                conexion.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ObjAsociado = new Asociado();

                    ObjAsociado.id = dr["id"].ToString();
                    ObjAsociado.nombre = dr["nombre"].ToString();
                    ObjAsociado.rol = dr["rol"].ToString();
                    ObjAsociado.estado = dr["estado"].ToString();
                    ObjAsociado.f_ult_ingreso = dr["f_ult_ingreso"].ToString();
                    ObjAsociado.f_ult_edicion = dr["f_ult_edicion"].ToString();
                }
            }
            catch (Exception ex)
            {
                ObjAsociado = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return ObjAsociado;
        }

        //Auditoria Sesión
        public bool AuditoriaSistema(int cod_proceso, string cod_usuario, String navegador, String pagina, String descripcion, String terminal_id)
        {
            bool resultado = false;

            SqlConnection conexion = null;
            SqlCommand cmd = null;

            try
            {
                //Instancia de Conexión
                conexion = ConexionDAO.getInstancia().ConexionBD();

                cmd = new SqlCommand("dbo.sp_web_auditoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parametros
                cmd.Parameters.AddWithValue("@cod_proceso", cod_proceso);
                cmd.Parameters.AddWithValue("@cod_usuario", cod_usuario);
                cmd.Parameters.AddWithValue("@navegador", navegador);
                cmd.Parameters.AddWithValue("@pagina", pagina);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@terminal_id", terminal_id);

                cmd.Parameters.AddWithValue("@proceso", 1);

                conexion.Open();

                int filas = (int)cmd.ExecuteNonQuery();

                if (filas > 0)
                {
                    resultado = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        public bool MantenimientoUsuario(Asociado usuario, String proceso_crud)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            bool resultado = false;

            try
            {
                conexion = ConexionDAO.getInstancia().ConexionBD();
                cmd = new SqlCommand("dbo.sp_web_crud_asociados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                if (proceso_crud == "W")
                {
                    cmd.Parameters.AddWithValue("@login", usuario.id);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@email", usuario.email);
                    cmd.Parameters.AddWithValue("@email2", usuario.email2);
                    cmd.Parameters.AddWithValue("@tel_habitacion", usuario.tel_habitacion);
                    cmd.Parameters.AddWithValue("@tel_celular", usuario.tel_celular);
                    cmd.Parameters.AddWithValue("@tel_trabajo", usuario.tel_trabajo);
                    cmd.Parameters.AddWithValue("@direccion", usuario.direccion);
                    cmd.Parameters.AddWithValue("@ubicacion_geografica", usuario.ubicacion_geografica);

                    DateTime fecha = DateTime.Parse(usuario.f_nacimiento, CultureInfo.CurrentCulture);
                    cmd.Parameters.AddWithValue("@f_nacimiento", SqlDbType.DateTime).Value = fecha;

                    cmd.Parameters.AddWithValue("@proceso_crud", proceso_crud);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@login", usuario.id);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@email", usuario.email);
                    cmd.Parameters.AddWithValue("@cod_rol", usuario.rol);
                    cmd.Parameters.AddWithValue("@clave", usuario.clave);
                    cmd.Parameters.AddWithValue("@i_estado", usuario.estado);
                    cmd.Parameters.AddWithValue("@proceso_crud", proceso_crud);
                }

                //Controlar Error de SQL
                //SqlParameter validacion = cmd.Parameters.Add("@validacion", SqlDbType.Int);
                //validacion.Direction = ParameterDirection.Output;

                conexion.Open();
                int filas = cmd.ExecuteNonQuery();  // Poner set nocount off en el SP

                if (filas > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        //GetUsuarios
        public List<Asociado> GetUsuarios()
        {
            List<Asociado> lista = new List<Asociado>();

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Asociado ObjAsociado = null;

            String proceso_crud = "R";

            try
            {
                conexion = ConexionDAO.getInstancia().ConexionBD();
                cmd = new SqlCommand("dbo.sp_web_crud_asociados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@proceso_crud", proceso_crud);

                conexion.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ObjAsociado = new Asociado();

                    ObjAsociado.id = dr["id"].ToString();
                    ObjAsociado.nombre = dr["nombre"].ToString();
                    ObjAsociado.rol = dr["rol"].ToString();
                    ObjAsociado.estado = dr["estado"].ToString();
                    ObjAsociado.email = dr["email"].ToString();
                    ObjAsociado.f_ult_ingreso = dr["f_ult_ingreso"].ToString();

                    //Añadir a la lista
                    lista.Add(ObjAsociado);
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

        //Agregado por Carlos Fonseca SOL271275 24/05/2019
        //se agrega el método para obtener la informacion de un usuario por su id. y tambien el metodo para el envío de correo.
        //GetUsuario
        public Asociado GetUsuario(string Login)
        {

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Asociado ObjAsociado = null;

            String proceso_crud = "S";

            try
            {
                conexion = ConexionDAO.getInstancia().ConexionBD();
                cmd = new SqlCommand("dbo.sp_web_crud_asociados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@login", Login);
                cmd.Parameters.AddWithValue("@proceso_crud", proceso_crud);

                conexion.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ObjAsociado = new Asociado();

                    ObjAsociado.id = dr["id"].ToString();
                    ObjAsociado.nombre = dr["nombre"].ToString();
                    ObjAsociado.rol = dr["rol"].ToString();
                    ObjAsociado.estado = dr["estado"].ToString();
                    ObjAsociado.email = dr["email"].ToString();
                    ObjAsociado.email2 = dr["email2"].ToString();
                    ObjAsociado.tel_habitacion = dr["tel_habitacion"].ToString();
                    ObjAsociado.tel_celular = dr["tel_celular"].ToString();
                    ObjAsociado.tel_trabajo = dr["tel_trabajo"].ToString();
                    ObjAsociado.direccion = dr["direccion"].ToString();
                    ObjAsociado.ubicacion_geografica = dr["ubicacion_geografica"].ToString();
                    ObjAsociado.f_ult_ingreso = dr["f_ult_ingreso"].ToString();
                    ObjAsociado.f_nacimiento = DateTime.Parse(dr["f_nacimiento"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                ObjAsociado = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return ObjAsociado;
        }


        public bool EnviarPassword(string nombre,string clave, string email)
        {
            bool resultado = false;

            SqlConnection conexion = null;
            SqlCommand cmd = null;

            try
            {
                //Instancia de Conexión
                conexion = ConexionDAO.getInstancia().ConexionBD();

                cmd = new SqlCommand("dbo.sp_web_envia_clave", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parametros
                cmd.Parameters.AddWithValue("@nom_usuario", nombre);
                cmd.Parameters.AddWithValue("@clave", clave);
                cmd.Parameters.AddWithValue("@email", email);

                var returnParameter = cmd.Parameters.Add("@resultado", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                conexion.Open();

                int filas = (int)cmd.ExecuteNonQuery();

                if((int)returnParameter.Value == 0)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }
        //Fin Agregado por Carlos Fonseca SOL271275 24/05/2019
    }
}
