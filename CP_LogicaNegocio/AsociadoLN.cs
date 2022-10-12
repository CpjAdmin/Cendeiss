using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP_AccesoDatos;
using CP_Entidades;

namespace CP_LogicaNegocio
{
    public class AsociadoLN
    {
        #region "Logica de Negocio Asociado"
        private static AsociadoLN ObjAsociado = null;
        private AsociadoLN() { }
        public static AsociadoLN getInstancia()
        {
            if (ObjAsociado == null)
            {
                ObjAsociado = new AsociadoLN();
            }
            return ObjAsociado;
        }
        #endregion

        // Acceso al Sistema
        public Asociado AccesoSistema(String Asociado, String clave)
        {
            Asociado AsociadoLogin = new Asociado();

            try
            {
                return AsociadoDAO.getInstancia().AccesoSistema(Asociado, clave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //*** AUDITORIA - Auditoria de Sesión
        public bool AuditoriaSistema(int cod_proceso, string cod_usuario, String navegador, String pagina, String descripcion, String terminal_id)
        {
            try
            {
                return AsociadoDAO.getInstancia().AuditoriaSistema(cod_proceso, cod_usuario,navegador,pagina,descripcion,terminal_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Mantenimiento de Asociado
        public bool MantenimientoUsuario(Asociado usuario, String proceso_crud)
        {
            try
            {
                return AsociadoDAO.getInstancia().MantenimientoUsuario(usuario, proceso_crud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get Usuarios
        public List<Asociado> GetUsuarios()
        {
            try
            {
                return AsociadoDAO.getInstancia().GetUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Agregado por Carlos Fonseca SOL271275 24/05/2019
        //se agrega el método para obtener la informacion de un usuario por su id. y tambien el metodo para el envío de correo.
        //Get Usuario
        public Asociado GetUsuario(string login)
        {
            try
            {
                return AsociadoDAO.getInstancia().GetUsuario(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool EnviarPassword(string nombre, string clave, string email)
        {
            try
            {
                return AsociadoDAO.getInstancia().EnviarPassword(nombre,clave,email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //FIN Agregado por Carlos Fonseca SOL271275 24/05/2019


    }
}
